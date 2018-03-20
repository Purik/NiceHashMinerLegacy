import argparse
import logging
import sys
import os
import glob
import datetime
import shutil


logging.basicConfig(format='%(levelname)s:  %(pathname)s \n\t%(message)s', level=logging.DEBUG, stream=sys.stdout)

BASE_DIR = os.path.dirname(__file__)


parser = argparse.ArgumentParser()
parser.add_argument('--version', type=str, required=True, help='Version.')
parser.add_argument('--bin_dir', type=str, required=True, help='Binary Directories.')
args = parser.parse_args()

cur_date = datetime.datetime.now().isoformat().split('T')[0]

# 1. Patch configs and meta files
for file in glob.glob('config/*.xml') + glob.glob('packages/com.cryptominer.app/meta/*.xml'):
    with open(file, 'r+') as f:
        logging.debug(f'Replace content file: "{file}"')
        content = f.read()
        content = content.replace('$teamcity-version', args.version)
        content = content.replace('$teamcity-release-date', cur_date)
        f.truncate(0)
        f.seek(0)
        f.write(content)
    pass


# 2. Copy bin directory
if not os.path.isdir(args.bin_dir):
    raise ValueError(f'Directory "{str(args.bin_dir)}" does not exists!')


PACKAGE_DIR = os.path.join(BASE_DIR, 'packages/com.cryptominer.app')
DATA_DIR = os.path.join(PACKAGE_DIR, 'data')
DISTR_DIR = os.path.join(DATA_DIR, 'bin')

if not os.path.isdir(DATA_DIR):
    os.mkdir(DATA_DIR)
shutil.copytree(args.bin_dir, DISTR_DIR)
