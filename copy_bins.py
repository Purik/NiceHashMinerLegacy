import argparse
import logging
import sys
import os
import glob
import shutil
import pathlib


logging.basicConfig(format='%(levelname)s:  %(pathname)s \n\t%(message)s', level=logging.DEBUG, stream=sys.stdout)

BASE_DIR = os.path.dirname(__file__)


parser = argparse.ArgumentParser()
parser.add_argument('--config', type=str, required=True, help='Configuration name.')
args = parser.parse_args()


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
