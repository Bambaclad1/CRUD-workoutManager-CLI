<img width="765" height="209" alt="c5a082cf-9e92-4ced-8eb1-467d3fb7e8b0" src="https://github.com/user-attachments/assets/9bdbf9e7-5b65-4bb6-9100-0297b4edc44a" />

# Just a CRUD Workout Manager

CRUD DotNet application allowing the user to perform CRUD operations with a application. This project is split in two phases:

- [ ] Phase 1: Create a CRUD application as a console application in C#
- [ ] Phase 2: Intregrate Avalonia UI Framework for a graphical application

So what does it do?
As a user, you can perform the following actions:
* Create a excercise through a guided step-for-step onboarding
* Retrieve excercises created saved in a .json format
* Create a workout, which can contain multiple excercises
* Retrieve a workout, saved in a .json as well
* Update excercises/workouts
* Remove excercises/workouts


## Contents
* (personal) To-Do List
* Tech Stack
* Create Opreations
	* Builder Pattern
	* The not so convenient solution
	* Saving data into JSON
* Read Operations
	* Saving excercise(s) into a JSON file
	* The Workouts to Excercise relationship
	* How did I approach this problem?
* Update Operations
* Remove Operations
* Implementing Avalonia UI into the console app
* Roadblocks I went through and learned from
* Disclaimer

## (personal) To-Do List
### Phase One
**'Backend' implementation**
- [x] Project initialization
- [x] Create A Excercise
- [x] Implement Builder Pattern at Create
- [x] Read Operation
- [x] Get Writing/Reading .json files working
- [x] Create Workout. One workout = multiple excercises
- [x] Read Workout. Same .json method
- [ ] Edit existing excercies (use the same builder pattern?)
- [ ] Edit existing workouts
- [ ] Remove Excercises/Workouts
- [ ] Create a Calendar (Use a package..?) and add the possibility to add workouts to it
- [ ] ICal Intregration

### Phase Two
**'Frontend' inplementation**
- [ ] Get familiar with Avalonia UI
- [ ] Do some Research on tool dashboard UI's. Find websites that offer UI examples
- [ ] Create a Figma Design for the frontend website. (Use Avalonia UI examples for the best success given time).
- [ ] Create the Front-End
- [ ] Design a dashboard-like interface.
## Tech Stack
Do people these days actually share their entire stack developing programs? I think it's nice to have in a documentation, just to let others know how I coded this program.

## Disclaimer
**Typo Disclaimer**: *I understand that seeing 'Excercise' and 'Exercise' in the code base is a huge mistake I've made, its pretty unprofessional. I will fix this in the front-end, but the Console App will stay like this for now.*
**AI Usage Disclaimer**: *AI was mostly used for help with complex parts such as serializing JSON, learning how Reflection works etc... aside from that, youtube videos, and Microsoft docs/GeeksForGeeks/W3Schools was used. Oh btw, ChatGPT created this cool bannner at the start of the page :)*

nvm this shit is terrible, never ever let ai make a banner bro 💀
<img width="855" height="543" alt="image" src="https://github.com/user-attachments/assets/baa28a92-8246-4e2f-a014-04ae378fb05d" />

wait actually, who tf let cgpt5 cook!? ts is majestic af <img width="64" height="64" alt="your wifi sucks" src="https://github.com/user-attachments/assets/e942f8ae-b5a1-40fb-9d8a-b15af8c0260c" />


<img width="1006" height="1058" alt="yeah skill issue if you see this tuff" src="https://github.com/user-attachments/assets/afe68fc1-51a8-4174-b1fc-09ab49f34e79" />
