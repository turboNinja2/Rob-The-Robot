from gensim.models import word2vec

sentences = word2vec.LineSentence('..//scraper//CK12.ency')

def hash32(value):
     return hash(value) & 0xffffffff
     
model = word2vec.Word2Vec(sentences, size=100, window=5, min_count=5, workers=4, seed=1, hashfxn=hash32)

