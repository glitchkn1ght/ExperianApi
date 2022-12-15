# ExperianApi

## Assumptions
  - That a photo only exists in a single album (given the schema i think this is a fairly safe one).
  
## Design Considerations
- The spec mentioned two endpoints, one for a certain userId and one which return all data. I have kept to this but the GetAll endpoint simply calls the other with a null userId as there didn't seem to be much benefit in having two completely different controller flows for such similiar requirements. At the service level, if a userId is supplied it simply modifies the resource url to only return data for that user. The downside to this is nothing in the code to prevent you just calling the Get(userid) endpoint with a null userId which may be undesirable depending on client requirements. Given more time i would find a way around this, e.g. GetAll can call the endpoint without a userId but external calls could not. 
- The mappers design is probably inefficient when processing larger amounts of data. I tried to mitigate this somewhat by sorting the lists and removing photos once they are mapped but feel there is probably a more elegant solution invoving Linq. 
- I wanted to keep response types consistent across all responses in order ease consumption by clients. Therefore i configured .net cores automatic 400 responses to use the same class as the others. This is somewhat new to me so i modified a source i found online, however this is a bit clunky and could be improved. 
- I differentiated objects received from the api to those included in responses to the client. I think this is generally considered good practice and i wanted make the names a bit more explicit but is perhaps unneccessary. 

## General Areas for improvement
  - More extensive unit testing to cover all classes and edge cases, particularly in the mapper which i feel could be broken with certain combinations of data.
  - Integration/PACT testing.
  - The class/method names are a bit unwieldy and could be more succinct.
