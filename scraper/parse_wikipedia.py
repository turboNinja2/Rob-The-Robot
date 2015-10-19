import wikipedia
import os

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
    
articles = [
    # geo
    "Geography",
    "North","South","East","West",
    "Continent","Asia","Africa","North America","South America","Antarcticta","Europe","Australia",
    "New York","London","Paris","Tokyo","Rome",
    "Pacific Ocean","Indian Ocean","Atlantic Ocean",
    
    #history
    "History", 
    "History of the United States","Christopher Columbus",
    "List of Presidents of the United States",
    "War","Peace",
    "Ancient history","Julius Caesar","Roman Empire",
    "Middle Ages",
    "Agriculture","Hunter-gatherer",
    
    #religion
    "God",
    "Monotheism","Polytheism",
    "Islam","Christianity","Judaism",
    
    #news
    "Newspaper","Pollution","Ecology",

    #bio
    "Male","Female","Gender",
    "Anatomy","Human body","Human head","Human brain","Nervous system","Smooth muscle tissue","Blood","Blood type",
    "Life","Organism","Cell (biology)","Biology","Bacteria","Microscope","Virus","DNA","Genetic code","Gene",
    "HIV",
    "Medicine","Vaccination",
    "Cat","Dog","Rabbit","Bird",
    "Fossil","Natural History", "Dinosaur",
    "Continental drift","Earthquake","Epicenter",
    "Magma","Volcano","Lava",
    "Sport",
    "Meat","Vegetables",
    "Plant","Tree","Grass","Fertilizer",
    "Protein","Amino acid",
    "Pregnancy","Embryo","Sexual reproduction",
    "Species","Mammal","Insect","Fish","Reptile",
    "Tetrapod","Vertebrate",
    "Food","Proteins","Vitamins","Minerals",
    
    
    #scientist
    "Science", "Scientist", "Researcher",
    
    #astrology
    "Universe", "Space", "Planet", "Star", "Comet", "Asteroid", "Galaxy", 
    "Earth", "Moon", "Sun",
    
    #hum
    "Desert", "Ocean", "Forest", "Fire", "Wind",
    
    #physics
    "Water","Properties of water",
    "Temperature",
    "Gas","Liquid","Solid",
    "Acid","Base (chemistry)",
    "Light","Prism","Optics","Rainbow",
    "Atom","Molecule","Electron","Subatomic particle","Particle",
    "Carbon","Hydrogen","Oxygen",
    "Energy",
    "Chemistry",
    "Electricity","Electric Charge","Electric Field","Electric Current","Electromagnetic radiation",
    "Magnetic field","Infrared",

    #techno
    "Computer","Robot","Car","Television","Screen","Internet","Integrated circuit",
    "Electronic component","Antenna (radio)",
    
    
    #literature
    "Literature","Novel","Prose","Book","Biography","Autobiography"]

    
for article in articles:
    print article # bufferd importation (to save wikipedia)
    if os.path.isfile('./articles/'+article+".txt"):
        print "-present"
    else:
        print "-download"
        import_clean_and_write(article)