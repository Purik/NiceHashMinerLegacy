import argparse
import logging
import sys
import os
import re
import glob
import shutil
import pathlib


logging.basicConfig(format='%(levelname)s:  %(pathname)s \n\t%(message)s', level=logging.DEBUG, stream=sys.stdout)

BASE_DIR = os.path.dirname(__file__)


parser = argparse.ArgumentParser()
parser.add_argument('--config', type=str, required=True, help='Configuration name.')
args = parser.parse_args()

# 1. Copy artifacts to config dir
for directory in ['AMDOpenCLDeviceDetection', 'setcpuaff']:
    SRC_DIR = os.path.join(BASE_DIR, directory, args.config)
    DST_DIR = os.path.join(BASE_DIR, args.config)
    logging.debug(f'SRC_DIR: "{SRC_DIR}"   DST_DIR: "{DST_DIR}"')
    for file in glob.glob(SRC_DIR + '/*.exe') + glob.glob(SRC_DIR + '/*.dll'):
        if '.vcxproj' in file:
            new_file = file.replace('.vcxproj', '')
            shutil.move(file, new_file)
            file = new_file
        file_ext = pathlib.Path(file).suffix
        logging.debug(f'COPY FILE: "{file}"   TO: "{DST_DIR}"')
        shutil.copy(file, DST_DIR)

# 2. Copy common DLLS to miners
NICEHASH_ORIG_PATH = os.path.join(BASE_DIR, 'NiceHashOriginal')
if not os.path.isdir(NICEHASH_ORIG_PATH):
    raise NotADirectoryError(f'Directory {NICEHASH_ORIG_PATH} does not exists!!!')
COMMON_DLL_PATH = os.path.join(NICEHASH_ORIG_PATH, 'common')
if not os.path.isdir(COMMON_DLL_PATH):
    raise NotADirectoryError(f'Directory {COMMON_DLL_PATH} does not exists!!!')
DISTR_PATH = os.path.join(BASE_DIR, args.config)
if not os.path.isdir(DISTR_PATH):
    raise NotADirectoryError(f'Directory {DISTR_PATH} does not exists!!!')
COMMON_DLLS = []
for file in glob.glob(COMMON_DLL_PATH + '/*.dll'):
    COMMON_DLLS.append(dict(
        path=file,
        filename=os.path.basename(file),
    ))
PATHS_WITH_EXE = []
for dirpath, dirnames, filenames in os.walk(DISTR_PATH):
    is_exe_exists = any([pathlib.Path(f).suffix == '.exe' for f in filenames])
    if is_exe_exists:
        exists_dlls = [f for f in filenames if pathlib.Path(f).suffix == '.dll']
        for dll in COMMON_DLLS:
            if dll['filename'] not in exists_dlls:
                shutil.copy(dll['path'], dirpath)
    pass

# 3. Copy common dir to distributive (like original miner)
src_dir = os.path.join(NICEHASH_ORIG_PATH, 'common')
dst_dir = os.path.join(DISTR_PATH, 'common')
shutil.copytree(src_dir, dst_dir)

# 4. Copy critical EXE and DLL from original distributive
src_dir = NICEHASH_ORIG_PATH
dst_dir = os.path.join(BASE_DIR, args.config)
excludes = ['(.*).lang', '(.*).pdf', '(NiceHashMinerLegacy.)']
for file in os.listdir(src_dir):
    full_path = os.path.join(src_dir, file)
    if os.path.isfile(full_path):
        do_copy = all([re.match(pattern, file) is None for pattern in excludes])
        if do_copy:
            src_path = full_path
            dst_path = os.path.join(dst_dir, file)
            shutil.copyfile(src_path, dst_path)
    pass
