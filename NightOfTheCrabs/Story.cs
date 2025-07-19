using static NightOfTheCrabs.Output;

namespace NightOfTheCrabs;

public static class Story
{
    internal static void TitleSequence()
    {
        TypeWriteLine("\t\tNight of the Crabs!");
        TypeWriteLine(
@"
                                      ...----:.                                                                  
                                  .:=*+=-.-==++#*.                                                               
                               .-+=:            :*-                                                              
                             :+=.    .::-===-    .+=                                                             
                           :+-    :=+****++**+-:.:=+.                                                            
                         :+:   .=*##*##%@%#*****#**+.                                                            
                  ..   .*:   :=*###%@@*-         -#*   :+                                                        
                 -%****=   :+*###%@%=             .#   =@:                                                       
                .+%%%=   .=*###%@%-                   .=+:                                                       
               =%%#*.   -=#%*#%@*                     .-+:                                                       
             .*+%#=.  .-+#%**%@*.                    .- *:                  .:-:::..                             
            =+#%%-    -+#@*+%@#.                    .+ -*.                -*++=*+=*+++#*#+*..                    
            *%-%+    .=*%#+#@@-                  .:*+.*%-               -#-..:        ---*%#+.                   
           *%%%%:    -+*%+*@@%:             .=+%%*-: #%-               :#-     -#%#*-    .=%%*.                  
          :###%%:    +#%#+%@@%=         .:+%+=**-   -*-               .++.  .+###***#*.  .-%%%%:                 
         :#+#*%@*. .+%@%+*@@@@#.        .-*+=+*+ :=**                .=+::*+=::-=+#%*==. -#%%*:.                 
        -  #%%%%%*=+#@@%=*@@@%%+. .. .--:=+=-. =%%%-                 :#+*-        =@%###%@%%%#-                  
        =%%%%%%%%%*#@@@%#@@%%@@@*=*%%#+++:   =#@%-                   .*:    ..-+=+#%@%%%%%%*%=..                 
         +%%%%%%%%%%%%%%@%%##+=+++-+-.    -+%@#.                     --  -##+%##**#@%%%%%%%%%*-.                 
        =%%%%%%%%%%%%%%%%%%=           +*%%%+                           +*##+     -%%%%%%%%%%%==.                
      =++%%%%%%%%%%%%%%%%@*:       -*#%@%=                             :**  :+*+==*%%%%%%%%%%%%%#.               
      =+-%%%%%%%%%%%%%%%%@#-    =*%@@%-                                =*--   %@@%%%%%%%%%%%#%##=.               
    -##%%%%%%%%%%%%%%%%%%@%-.---+#%%                                   :+     ++%%%%%%%%%%%%%%%%-                
   +%%##%*%%%%%%%%%%%%%%%%@@%###%@#                                           +#%%%%%%%%%%##*#%#+:               
  .#++%%%**%%%%%%%%%%%%%%%%%%@@@@:       .+%%%%%%####**--::=*+.                .%%%%%%%%%%#%%%%%%#.              
  *%%%%%%%%%%%%%%%%%%%%%%%%%%%+-.    :+.:#-+#%%%%%%%%%%%%%%**%#%%%%%%%%%%%-      :+#%%%%%%%**+%%%%%%*            
 .+*##%**%%%%%%%%%%%%%%%%%%%%*%%##*:*#%%%=.    #%%%+#%%%%%%%%%%%%%%%%%%%%%%#+.*-  :%%%%%%%%##%%%%%%#             
*%#:#%%%%%%%%%%%#++##*%##   .%%-=%%%% :++:=-..-.:..-..+%%%%%%%- =#%%*. +%%%%%%%%#%%%%%%%%%%%%%%%%%%%%=           
%==:%%%%#%%%%%%%%%%%%%+.  +%%%%%%%%+::=%@#+--%@@@@+#-:...:#%+.*= ..=#+.  ::#%%%%%%##%%%%%%*%%%%%%%*-:%%+         
.#=%%%#+*%%%%***##%%%%#%%%%=:+=-++:.=.%%-.-@=...**..#==.........==..:**%%%%%%%%%+%*#%%%%%%%%%#=..#%%.            
:*%%%%%*+###%%%**-+#*#*#%%%%%*:-...+.*.#* :*-:=*+.#..+=....:.....#*##*+%%%=%--%+:+-:=+%#%#*---.-%%%%.            
#%#-*%%%%%%*+#**-==:..+%.=*%%%%%+=-.+...+#####%=+++-:**.....:.=%##=:::=-+:..%:.+:--=--*%%####***%%*              
   .#%%%%@%#*#++-...--.....:*-*%%%#++:.++*##%#=.:-:#*-=......*=+=..%*..........:::-=++#%%%%#*@*:**               
  -#%%-. =#%%%%%#+=-.=..-....=--+**%:***#%**%##**-%++=+.:.-=-##%=....-....*%..+=+*#*#%%+..-#*.                   
             =%#%#*====......=:+-:-*#=-#####**##=+#++*..--..:#+*:=::.=*+.=##-==*#%%+-...=*#%=                    
                 :+=-:..:%...*=:.-+*#*=:.=+=:=-:=...-=:...:.::#+:=*+-=**+=++++*#%+:---=-..:#@#.                  
           .=*+*+:=%#*+=-.=--=*+==-+%*+--:....-.......::-==...++**====++**##=-.:-:......:-+**==.  -.             
         .#**=*++=-=*%#+*=*=-=+===++*%=+***#+===-=====-.:...-=-+*#**+*#**#*+++++==-==++=++-#-:+#.-#*-.           
        =-. .*+--+=::+**#+**+++*****#*==:......==::.......:-.-++++*%@%#+..-=#+=*++=++-*+++**=..=**%*+:*          
      .=-=+=*%%*=-:..:-++***#*++*#%%%*++++--==*##+**#*+=-==-.++:-:=%#*=. :... ...*+:::-=++*%+-.-**++==.:.        
     .=+=-==*..+==+*-.:-.-**+*##+:-*=#*=+=..::...-++-::.....:.:==++##*#*+++=+++=+*--=+*+*#%%#-..+*#%-=:-+-       
     -+--=*+ .==-*+--**-...-+-.-:..:-:#*+*+*#**+==-+*=-:::-=--=-=-==-+##*==---%%##*+=**-:=*+#*=-+++%*+*=+++-     
     :..=#=  -+-=+*#*=+#*==%==---=+++**%#%*==----===%*=+==-:.-.-:-=+=*##*=+#*#**#*++##*##--=#=:.-= -#**.:=**.    
   .*-:=**   =.*++=..:*###*%+#*+*#*==-:+%+*###****#%#%%%###*==++====+*%%*:..=#++=+**#%*====**=:--+.  -#=.::::    
   .=.-++   .#.:=+.   .++#*%###+===--=**#*+*#%%#%@#*#*+=+*=-=++=#%#*##**+++==----=+==-::=++%#++-.++-  :++*#+-=.  
  +*:-=*=   #*=+=+.   +--.=*###+++=+*%%#+++##*=+*#%@#+-+##+=*#-::.-+%*+=-.-#+++**##+=+**#-.:#++-.=*-+. =+.-+==+  
  =-===+=   =--+#*-  +=:.=++===**++##%@***###+=**=:.     ........   :+#**=-+*:+***%#*#**   -*+==+**+:* :*--+=.:+ 
  *. =**#-  -+-=#=  -+**-++-   .-.=+#%*%#**#%#**#                     :**+**@**-=+#%#++%. .**++*::++=:- =+=*+  -+
  .  :**+#.  = -#+. -+-=+*      ==++%.  .=###%%-                       .-#%@#*.   :#**:+:  *=+#+  +#*: = .*#%* . 
  .: =%%%%.  - =%%:::-*%+.      =###=.      .                                     .*%*.*   *###*.-#%%*--- .=*#=: 
  .= =#:.:   .=-%*:--*-.        -*#.                                               -%:.#   :*=*+:-+++%#.=    .*- 
   =:=#.      -.#: :-#          .=#                                                :#.+-     .+-:+:  :*#--    =* 
   .+=+.      .--= .-#           =*                                                -=-.      .=-=-     ++::   .* 
   -+-        -*: -#.           =.                                              .::        .-:=.      --.    +   
      .-=.       .=. :                                                           .-.         .:-.        :.      
         .                                                                                  .--.                 
                                                                                            ..                  ");
        TypeWriteLine("\t\tA text adventure based on the novel by Guy N. Smith");
    }

    internal static void IntroText()
    {
     TypeWriteLine(@"
You are Cliff Davenport — marine biologist, man of science, and now, unwilling mourner.
The letter arrived three days ago, soaked in salt and sorrow. Your nephew Ian, along with his companion Julie, has vanished along the Welsh coast, near the brooding village of Barmouth. The official story is simple, convenient: an accident, the sea taking what it always does. But you know the sea. You’ve charted its moods, dissected its mysteries, listened to the rhythm of its tides like a man listens to his own heartbeat. And something in the story does not ring true.

There were marks—deep gouges in the sand like the dragging of claws. Whispers among the villagers of strange shapes moving in the surf at dusk. Mutilated livestock. Silence where there should be birdsong. You've read the signs, smelled the metallic tang of fear in the air.
This is no accident.

Your grief sharpens into resolve. You feel the old fire stirring—the need to understand, to hunt, to destroy what should not exist. The authorities offer platitudes and urge you to leave things be, but you've never trusted men who speak softly over sealed files.
And so you prepare.

With a satchel of field gear and a battered notebook, you board a train bound for the coastal outpost of Llanbedr, a place carved into the cliffs and haunted by tides older than memory. Locals avoid its beaches. Fishermen speak of colossal shadows moving beneath the waves. The air there tastes of brine and blood.
You are no longer just a biologist.

You are a man with nothing to lose — and monsters to fight.");
    }

    internal static void PostIntro()
    {
     TypeWriteLine(@"
The living room is quiet but for the low tick of the mantel clock, each second like the measured beat of a pulse. Outside, the late afternoon dims into the cold wash of twilight. You sit in your high-backed chair, one hand resting on the armrest, the other curled loosely around a tumbler of untouched whisky. The drink is for show, mostly — a gesture to civility in the face of chaos.

Grief lingers at the edge of your thoughts, a cold shadow pressing at the corners of your mind. But you do not let it in. Grief is a luxury you cannot yet afford. Instead, you turn your focus inward — cataloguing facts, weighing possibilities, mentally assembling the puzzle from scattered, blood-streaked pieces.
You stare at the fire, not seeing it.

Ian is gone. That much is certain. But the sea doesn’t leave claw marks in the sand. And animals don’t simply vanish without a trace. Something is wrong — unnaturally wrong. And that thought does not terrify you. It intrigues you.
You are not a man who succumbs to hysteria. You are a man of logic, discipline, and quiet conviction. The unknown does not frighten you — it beckons.");
    }
}