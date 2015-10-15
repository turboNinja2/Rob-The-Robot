import wikipedia

def import_raw_article(article_name) :
    ny = wikipedia.page(article_name)
    return ny.content.encode('ascii', 'xmlcharrefreplace')

def remove_empty_lines_and_titles(article):
    splitted_article = article.split('\n')
    splitted_article = filter(lambda x: len(x) > 10, splitted_article)
    splitted_article = filter(lambda x: not x.startswith('==') , splitted_article)
    return '\n'.join(splitted_article)
    
def import_clean_and_write(article_name):
    f = open('./articles/' + article_name+'.txt', 'w')
    f.write(remove_empty_lines_and_titles(import_raw_article(article_name)))
    
import_clean_and_write("New York")
import_clean_and_write("Sport")
import_clean_and_write("Cat")
import_clean_and_write("Dog")
import_clean_and_write("Smooth muscle tissue")
import_clean_and_write("Blood")
