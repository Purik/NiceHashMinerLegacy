import os
import glob
import argparse

import requests
from YaDiskClient.YaDiskClient import YaDisk


URL = 'https://freezone.name/ajax/update-software-version'
YaDiskDir = '/Miner'

parser = argparse.ArgumentParser()
parser.add_argument('--secret', help='Secret token for server software version update')
parser.add_argument('--login', help='YaDisk login')
parser.add_argument('--password', help='YaDisk password')
parser.add_argument('--soft_version', help='Software version')
parser.add_argument('--base_dir', required=True, help='Base directory')
args = parser.parse_args()
BASE = args.base_dir
print(f'BASE DIR: "{BASE}"')


disk = YaDisk(args.login, args.password)
exe_file = None
for f in glob.glob(BASE + '\*.exe'):
    exe_file = f

if exe_file:
    deployed_exes = [x['path'] for x in disk.ls(YaDiskDir) if x['path'][-4:] == '.exe']
    src = os.path.realpath(exe_file)
    dst = YaDiskDir + '/' + os.path.basename(exe_file)
    disk.upload(src, dst)
    try:
        public_url = disk.publish(dst)
        resp = requests.post(URL, dict(token=args.secret, version=args.soft_version, download_link=public_url))
        assert resp.json()['success'] is True
    except:
        disk.rm(dst)
    else:
        for f in deployed_exes:
            disk.rm(f)
else:
    raise EnvironmentError('Exe file list is EMPTY!!!')
