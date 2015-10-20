import wikipedia
import os

def import_raw_article(article_name) :
    ny = wikipedia.page(article_name)
    return ny.content.encode('ascii', 'xmlcharrefreplace')

def remove_empty_lines_and_titles(article):
    splitted_article = article.decode('ascii').split('\n')
    splitted_article = filter(lambda x: len(x) > 10, splitted_article)
    splitted_article = filter(lambda x: not x.startswith('==') , splitted_article)
    return '\n'.join(splitted_article)
    
def import_clean_and_write(article_name):
    f = open('./articles/' + article_name+'.txt', 'w')
    f.write(remove_empty_lines_and_titles(import_raw_article(article_name)))
    
articles = [
    # geo
    "Geography",
    "Map","North","South","East","West",
    "Continent","Asia","Africa","North America","South America","Antarcticta","Europe","Australia",
    "New York","London","Paris","Tokyo","Rome",
    "Pacific Ocean","Indian Ocean","Atlantic Ocean",
    "India","China","Japan","France",
    
    #history
    "History", 
    "History of the United States","Christopher Columbus",
    "List of Presidents of the United States",
    "War","Peace",
    "Ancient history","Civilization",
    "Julius Caesar","Roman Empire",
    "Ancient Greece",
    "Middle Ages",
    "Agriculture","Hunter-gatherer",
    
    #Politics
    "Democracy","Monarchy","Dictatorship",
    "Nation",
    
    #Economics
    "Economics","Market (economics)",
    
    #religion
    "God",
    "Monotheism","Polytheism",
    "Islam","Christianity","Judaism",
    
    #news
    "Newspaper","Pollution","Ecology",

    #bio
    "Biologist","Biology",
    "Male","Female","Gender",
    "Anatomy","Human body","Human head","Human brain","Nervous system","Smooth muscle tissue",
    "Blood","Blood type","Circulatory system",
    
    "Life","Organism","Cell (biology)","Biology","Bacteria","Microscope","Virus","DNA","Genetic code","Gene",
    "HIV",
    "Medicine","Vaccination",
    "Cat","Dog","Rabbit","Bird","Elephant",
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
    "Food","Proteins","Vitamins","Minerals","Poison",
    
    
    #scientist
    "Science", "Scientist", "Researcher","Analysis",
    
    #astrology
    "Universe", "Space", "Planet", "Star", "Comet", "Asteroid", "Galaxy", 
    "Earth", "Moon", "Sun",
    
    #Climate
    "Desert", "Ocean", "Forest", "Fire", "Wind", "Iceberg",
    "Rain","Cave", "Geology",
    "Natural resource","Fossil fuel",
    "Atmosphere of Earth","Ozone",
    
    #mathematics
    "Mathematics","Calculus","Statistics","Geometry","Square","Circle",
    
    #physics
    "Water","Properties of water",
    "Temperature",
    "Gas","Liquid","Solid",
    "Acid","Base (chemistry)",
    
    "Light","Prism","Optics","Rainbow",
    "Atom","Molecule","Electron","Subatomic particle","Particle",
    
    "Carbon","Hydrogen","Oxygen","Calcium","Helium","Nitrogen",
    "Aluminium","Gold","Silver",
    
    "Carbon dioxyde","Carbon monixide",
    
    "Energy",
    "Chemistry",
    "Electricity","Electric Charge","Electric Field","Electric Current","Electromagnetic radiation",
    "Magnetic field","Infrared",
    "Oil","Petroleum",

    #techno
    "Computer","Robot","Car","Television","Screen","Internet","Integrated circuit",
    "Telephone",
    "Electronic component","Antenna (radio)",
    "Vehicle","Airplane", "Train", "Boat",
    "Road","Highway",
    
    #literature
    "Verb","Noun","Grammar","English Grammar",
    "Literature","Novel","Prose","Book","Biography","Autobiography"]

    
for article in articles:
    print(article) # bufferd importation (to save wikipedia)
    if os.path.isfile('./articles/'+article+".txt"):
        print("-present")
    else:
        print("-download")
        import_clean_and_write(article)