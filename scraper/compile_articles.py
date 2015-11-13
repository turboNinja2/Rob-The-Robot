import shutil, glob

outfilename = "Wikipedia.ency"

with open(outfilename, 'wb') as outfile:
    for filename in glob.glob('./*.ency'):
        if filename == outfilename:
            # don't want to copy the output into the output
            continue
        with open(filename, 'rb') as readfile:
            shutil.copyfileobj(readfile, outfile)