import argparse
import logging
import sys
import os
import glob
import datetime
import shutil
import re

import requests


logging.basicConfig(format='%(levelname)s:  %(pathname)s \n\t%(message)s', level=logging.DEBUG, stream=sys.stdout)

BASE_DIR = os.path.dirname(__file__)


parser = argparse.ArgumentParser()
parser.add_argument('--version', type=str, required=True, help='Version.')
parser.add_argument('--secret', help='Secret token for server software version update')
args = parser.parse_args()


URL = 'https://freezone.name/ajax/nicehash-software-version'
assembly_file = os.path.realpath(os.path.join(BASE_DIR, '../NiceHashMiner/Properties/AssemblyInfo.cs', ))
with open(assembly_file, 'r+') as f:
    content = f.read()

    search = re.search('AssemblyVersion(\(")\d.\d.\d.\d"\)', content).group(0)
    orig_version = re.search('\d.\d.\d.\d', search).group(0)
    resp = requests.post(URL, dict(version=orig_version, token=args.secret))

    find = 'AssemblyVersion(.)*'
    replace = f'AssemblyVersion("{args.version}")]'
    content = re.sub(find, replace, content)

    find = 'AssemblyFileVersion(.)*'
    replace = f'AssemblyFileVersion("{args.version}")]'
    content = re.sub(find, replace, content)

    content = re.sub('NiceHashMinerLegacy', 'CryptoMiner', content)
    content = re.sub('NiceHash', 'CryptoMiner', content)

    f.seek(0)
    f.truncate(0)
    f.write(content)
