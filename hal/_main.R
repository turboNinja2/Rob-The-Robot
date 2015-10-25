library(kernlab)

data(reuters)
y <- rlabels

x <-scan("./clean.csv", what = "list")

x

require(tm)


x <- Corpus(DirSource(".//clean.csv"), readerControl = list(language="eng"))

head(reuters)

sk <- stringdot(type="spectrum", length=2, normalized=TRUE)
kpc <- kpca(x,kernel=sk,scale=c())
plot(rotated(kpc),col=ifelse(y==levels(y)[1],1,2))

sgk <- gapweightkernel(length=2,lambda=0.75,normalized=TRUE,use_characters=TRUE)
kpc <- kpca(x,kernel=sgk,scale=c())
plot(rotated(kpc),col=ifelse(y==levels(y)[1],1,2))
dataPath <- "C:\\Users\\JUJulien\\Desktop\\KAGGLE\\Competitions\\Rob-The-Robot\\data\\"

questionsTrain <- read.table(paste0(dataPath,"training_set.tsv"),
                             sep = '\t',
                             header=TRUE,
                             fill=TRUE)

head(questionsTrain)


require(tm)

txt <- system.file("texts", "txt", package = "tm")

questions <- VCorpus(DirSource(dataPath, encoding = "UTF-8"),readerControl = list(language = "lat"))
questions <- tm_map(questions, content_transformer(tolower))
questions <- tm_map(questions, removeWords, stopwords("english"))

summary(questions)
