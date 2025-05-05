EXTERNAL loadScene(sceneName)
// I THINK this is how the variable call should look
VAR docSet1 = 1
//This all works, you just need to replace the two with something that reads a variable from unity on how many docs we picked up.
{ docSet1:
- 1: -> 1stInt1 
}


==1stInt1==
We arrive back home, weary and tired. I’ve never done anything quite like that before. # playerthoughts
Janet: We have to go to the police. # Janet
Hunter: Are you crazy? They’ll arrest us for breaking into that place. # hunter
Janet Not if we’re blowing the whistle on them. What kind of abandoned warehouse needs that kind of security? # Janet
Hunter: One that we shouldn’t get involved in anymore. # hunter
Player: Before we do anything, we should look at what we recovered. # player
Janet: Well, we didn’t find much. # Janet
Hunter: Oh yeah, what did we grab? # hunter
Player: This looks like… a memo? To employees of “the Echelon Institute.” I don’t know what that means. # player
Janet: I’m looking them up now. # Janet
Janet: ... # Janet
Janet: ... # Janet
Janet: Okay, found them. # Janet
Player: What does it say, Janet? # player
Janet: There’s not a whole lot… but they’re a political organization. Think policy and such. They say they’re not affiliated with any particular group, but they’re pretty aligned with… reactionary elements. # Janet
Hunter: Reactionary elements? Like lithium? # hunter
Janet: Lithium? What do you- No, not like that. As in, people who don’t like change. And they get really extreme about it. # Janet
Player: And my mom’s been taken by these people? # player
Janet: Probably? # Janet
Player: I think we need to go back there. # player
Hunter: WHAT! # hunter
Janet: [Player], you’re joking, right? # Janet
Player: We need to learn more. # player
Janet: The police will- # Janet
Player: The police will take too long. Who knows what could happen to my mom in that time? # player
Janet: ... # Janet
Janet: You’re right. # Janet
Hunter: Wait, Janet, you’re supposed to be the reasonable one! You’re supposed to say no to stuff like this. # hunter
Janet: [Player] makes a convincing point- the authorities will be slower to act than us. # Janet
Hunter: Well, if you two are going to do this anyway, I guess I’m still in, too. # hunter
Janet: Good. [Player], we’ll reconvene at the warehouse tomorrow afternoon. # Janet
With that, my friends take their leave. # playerthoughts

-> WarehouseExt2
==WarehouseExt2==
I drift into a fitful sleep, the events of the past day replaying themselves in my head. # playerthoughts
This strange and secretive organization, what are their motives? How is my mom wrapped up in all this? # playerthoughts
Whatever the case, when I awaken in the morning, I barely feel rested, and in the afternoon I make my way over to the warehouse. # playerthoughts
Janet: [Player]! You ready to head in? # Janet
Hunter: Wait, what if I’m not ready? # hunter
Janet: Hunter, you’ve been here for 10 minutes with me. If you weren’t ready, you would have said so. # Janet
Hunter: I guess… # hunter
Janet: So, [Player], shall we head on in? # Janet
Player: Let’s do this. # player
~ loadScene("warehouse2")
    -> END

