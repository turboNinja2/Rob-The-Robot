from stemming.porter2 import stem
from gensim.models import word2vec

encyclopedia_name = 'CK12'

# performs stemming and stamps the new file

print('Import file')
opened_file = open('..//scraper//' + encyclopedia_name + '.ency', encoding='utf-8')
with open('.//temp//' + encyclopedia_name + 'stemmed.ency', 'wb') as outfile:
    for t, line in enumerate(opened_file):
        try:
            stemmed_line = ' '.join(stem(x.strip()) for x in line.split(' '))
            if(len(stemmed_line)>1):
                outfile.write(stemmed_line + '\n')
        except:
            pass
    
print('Stemmed')

sentences = word2vec.LineSentence('.//temp//' + encyclopedia_name + 'stemmed.ency')
sentences.build_vocab([s.encode('utf-8').split( ) for s in sentences])

# learns word2vec

def hash32(value):
     return hash(value) & 0xffffffff
     
model = word2vec.Word2Vec(sentences, size=100, window=5, min_count=5, workers=4, seed=1, hashfxn=hash32)

model.save('.//temp//' + encyclopedia_name + '.w2v')

