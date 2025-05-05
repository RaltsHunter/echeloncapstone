EXTERNAL loadScene(sceneName)

-> opening
==opening==
I don’t know what I was expecting. Nothing in the news is ever good these days. # playerthoughts
I wonder when mom is gonna be back… Things are starting to get scary out there. # playerthoughts
[Knock-knock] # ambient

I think someone might be at the door # playerthoughts
[BANG] # ambient
Or in the house now! I should go check on that. # playerthoughts

//next line should trigger when the player enters the living room
Player: Janet! Hunter! # player 
Hunter: Hey [player], we wanted to stop by and hang. # hunter
Janet: Someone decided they should let themselves in… # Janet
Player: Well I always love seeing you guys. Even if I do prefer letting you in myself… # player
Janet: How was your day? # Janet
-> day_choice
==day_choice==
* Fine # player
-> after_day_choice
* So-so # player
-> after_day_choice
* Not bad # player
-> after_day_choice
==after_day_choice==
Janet: Well at least your day hasn’t been bad! # Janet
Hunter: Oh, [Player], where’s your mom? I was hoping she could make us some of her world famous cookies. # hunter
Janet: You pig. # Janet
Hunter: What?! # hunter
Janet: We all know you have a crush on [player]’s mom. # Janet
Hunter: I- I… I do not! And I resent that! # hunter
Janet: I’d like to apologize for Hunter’s behavior. # Janet
Hunter: You can’t apologize for me, Janet! # hunter
Janet: Then apologize for yourself. # Janet
Player: Whoa, guys, calm down. No one needs to do any apologizing. # player
Hunter: Thank you. # hunter
Player: But to be honest, I haven’t seen my mom for a few days. # player
Janet: Like, she’s been busy at work? # Janet
Player: No, she’s just been… gone? I haven’t heard from her, either. # player
And I’m kind of scared… # playerthoughts
Hunter: That doesn’t sound good. # hunter
Janet: You don’t need to state the obvious, dimwit. # Janet
Hunter: Hey! # hunter
Player: Guys, cut it out. # player
Janet: Yeah, we need to be focused, Hunter. # Janet
Hunter: Sigh # hunter
Hunter: Do you have a way you could find out where your mom is? # hunter
Player: I don’t know… # player
Janet: Does she have GPS in her car? # Janet
Player: Yeah? # player
Janet: Then we can use your phone to ping it. Give me your phone [Player]. # Janet
There’s no real point in resisting Janet when she gets like this, so I hand over my phone. # playerthoughts
Janet: One sec, annnnnnnd we’re in. Yup, your mom’s car is… in the warehouse district? Weird. # Janet
Hunter: Does your mom go to raves in warehouses? Sick. # hunter
Player: No, I… I don’t know why mom would be there. # player
Janet: We should go investigate. # Janet
Hunter: Yeah!
-> investigate_choice
==investigate_choice==
*Alright! # player
-> investigate_confirmed
*If you both think we should, why not? # player
-> investigate_confirmed
*I guess I don’t have anything better to do… # player
-> investigate_confirmed
==investigate_confirmed==
Janet: Awesome! We get to be like detectives. # Janet
Hunter: Or spies! # hunter
Janet: Hunter, that’s silly. # Janet
Hunter: How is that any more silly than being detectives! # hunter
Player: Guys, we should really get going. It’s starting to get dark out. # player
Janet: Let’s move out! # Janet
Hunter: Reporting for duty. # hunter
I don’t know what it is, but it feels like something bad is going to happen… # playerthoughts
-> WarehouseExt1

//Triggers at warehouse
==WarehouseExt1==
The warehouse district is pretty scary, even during the day. And it’s almost nighttime. # playerthoughts
Hunter: There’s your mom’s car! # hunter
Janet: Kinda weird that you could recognize her car so fast. # Janet
Hunter: That’s not fair! I carpool with [Player] a lot! # hunter
Janet: Likely story. # Janet
Player: Focus, you two. # player
To be completely honest, their banter is keeping me from freaking out. # playerthoughts
Hunter: Well, she’s not here. # hunter
Janet: Obviously. We’re going to have to go into the warehouse. # Janet
Hunter: Wait what? How do we know this is even the right one? # hunter
Player: I mean, all the other warehouses on the street are boarded up. This one has lights on inside. # player
Hunter: That… that is a good point. # hunter
Janet: Come on dorks, time’s wasting. You got your magic ready, [Player]? # Janet
Player: Of course, ready for anything.# player
Oh boy, here we go. # playerthoughts
->WarehouseInt1
//Triggers in warehouse
==WarehouseInt1==
Hunter: It’s kinda creepy in here. # hunter
Janet: That doesn’t matter. We need to be quick, and find anything we can about [Player]’s mom. # Janet
Hunter: Quick… okay, I can get behind that. The sooner we’re outta here the better! # hunter
Janet: And we have to be ready to fight if we need to. # Janet
Player: Fight? # player
Janet: We’re not supposed to be here. And if whoever’s running this place took your mom, they’re probably going to want to deal with us themselves. # Janet
Hunter: Is it too late to back out?
# hunter

~ loadScene("SampleScene")


    -> END
