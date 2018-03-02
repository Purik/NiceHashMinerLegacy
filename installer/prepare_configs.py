import argparse
import logging
import sys
import os
import glob
import datetime
import pathlib
import shutil


logging.basicConfig(format='%(levelname)s:  %(pathname)s \n\t%(message)s', level=logging.DEBUG, stream=sys.stdout)

BASE_DIR = os.path.dirname(__file__)


parser = argparse.ArgumentParser()
parser.add_argument('--version', type=str, required=True, help='Version.')
args = parser.parse_args()

cur_date = datetime.datetime.now().isoformat().split('T')[0]

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
