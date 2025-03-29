˜
n/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/AppRestriction/Bidings/IAppRestrictionBinding.cs
	namespace 	
AppRestriction
 
. 
Bindings !
{ 
public 

	interface "
IAppRestrictionBinding +
{ 
List 
< 
ApplicationInfo 
> 
GetInstalledApps .
(. /
)/ 0
;0 1
List		 
<		 
string		 
>		 
GetRunningAppsNames		 (
(		( )
)		) *
;		* +
}

 
} Ÿ
d/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Notifications/ISupressNotifications.cs
	namespace 	
Notifications
 
{ 
public 

	interface !
ISupressNotifications *
{ 
void #
SupressAllNotifications $
($ %
)% &
;& '
void *
AskForNotificationPolicyAccess +
(+ ,
), -
;- .
bool -
!IsNotificationPolicyAccessGranted .
(. /
)/ 0
;0 1
} 
}		 …5
R/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Login/AuthManager.cs
public 
class 
AuthManager 
: 
MonoBehaviour (
{ 
public		 

static		 
AuthManager		 
Instance		 &
{		' (
get		) ,
;		, -
private		. 5
set		6 9
;		9 :
}		; <
[

 
SerializeField

 
]

 
private

 
string

 #
loginEndpoint

$ 1
=

2 3
$str

4 V
;

V W
private 
void 
Awake 
( 
) 
{ 
if 

( 
Instance 
== 
null 
) 
{ 	
Instance 
= 
this 
; 
DontDestroyOnLoad 
( 

gameObject (
)( )
;) *
} 	
else 
{ 	
Destroy 
( 

gameObject 
) 
;  
} 	
} 
public 

async 
void 
HandleLogin !
(! "
string" (
username) 1
,1 2
string3 9
password: B
,B C
ActionD J
	onSuccessK T
=U V
nullW [
,[ \
Action] c
<c d
stringd j
>j k
	onFailurel u
=v w
nullx |
)| }
{ 
using 
( 

HttpClient 
client  
=! "
new# &

HttpClient' 1
(1 2
)2 3
)3 4
{ 	
try 
{ 
	LoginData 
	loginData #
=$ %
new& )
	LoginData* 3
{4 5
username   
=   
username   '
,  ' (
senha!! 
=!! 
password!! $
}"" 
;"" 
string$$ 
json$$ 
=$$ 
JsonUtility$$ )
.$$) *
ToJson$$* 0
($$0 1
	loginData$$1 :
)$$: ;
;$$; <
var%% 
content%% 
=%% 
new%% !
StringContent%%" /
(%%/ 0
json%%0 4
,%%4 5
Encoding%%6 >
.%%> ?
UTF8%%? C
,%%C D
$str%%E W
)%%W X
;%%X Y
HttpResponseMessage'' #
response''$ ,
=''- .
await''/ 4
client''5 ;
.''; <
	PostAsync''< E
(''E F
loginEndpoint''F S
,''S T
content''U \
)''\ ]
;''] ^
string(( 
responseJson(( #
=(($ %
await((& +
response((, 4
.((4 5
Content((5 <
.((< =
ReadAsStringAsync((= N
(((N O
)((O P
;((P Q
APILoginResponse**  
apiResponse**! ,
=**- .
JsonUtility**/ :
.**: ;
FromJson**; C
<**C D
APILoginResponse**D T
>**T U
(**U V
responseJson**V b
)**b c
;**c d
if,, 
(,, 
response,, 
.,, 
IsSuccessStatusCode,, 0
&&,,1 3
!,,4 5
string,,5 ;
.,,; <
IsNullOrEmpty,,< I
(,,I J
apiResponse,,J U
.,,U V

acessToken,,V `
),,` a
),,a b
{-- 
Debug.. 
... 
Log.. 
(.. 
$str.. 3
)..3 4
;..4 5

SaveTokens// 
(// 
apiResponse// *
.//* +

acessToken//+ 5
,//5 6
apiResponse//7 B
.//B C
refreshToken//C O
)//O P
;//P Q
	onSuccess00 
?00 
.00 
Invoke00 %
(00% &
)00& '
;00' (
}11 
else22 
{33 
if55 
(55 
response55  
.55  !

StatusCode55! +
==55, .
System55/ 5
.555 6
Net556 9
.559 :
HttpStatusCode55: H
.55H I
	Forbidden55I R
)55R S
{66 
Debug88 
.88 
LogError88 &
(88& '
$str88' 7
)887 8
;888 9
	onFailure99 !
?99! "
.99" #
Invoke99# )
(99) *
$str99* :
)99: ;
;99; <
};; 
else<< 
{== 
Debug>> 
.>> 
LogError>> &
(>>& '
$">>' )
$str>>) /
{>>/ 0
response>>0 8
.>>8 9

StatusCode>>9 C
}>>C D
">>D E
)>>E F
;>>F G
	onFailure?? !
???! "
.??" #
Invoke??# )
(??) *
$"??* ,
$str??, 2
{??2 3
response??3 ;
.??; <

StatusCode??< F
}??F G
"??G H
)??H I
;??I J
}@@ 
}AA 
}BB 
catchCC 
(CC  
HttpRequestExceptionCC '
exCC( *
)CC* +
{DD 
DebugEE 
.EE 
LogErrorEE 
(EE 
$"EE !
$strEE! 2
{EE2 3
exEE3 5
.EE5 6
MessageEE6 =
}EE= >
"EE> ?
)EE? @
;EE@ A
	onFailureFF 
?FF 
.FF 
InvokeFF !
(FF! "
$"FF" $
$strFF$ 5
{FF5 6
exFF6 8
.FF8 9
MessageFF9 @
}FF@ A
"FFA B
)FFB C
;FFC D
}GG 
}HH 	
}II 
privateKK 
voidKK 

SaveTokensKK 
(KK 
stringKK "
tokenKK# (
,KK( )
stringKK* 0
refreshTokenKK1 =
)KK= >
{LL 
PlayerPrefsMM 
.MM 
	SetStringMM 
(MM 
$strMM )
,MM) *
tokenMM+ 0
)MM0 1
;MM1 2
PlayerPrefsNN 
.NN 
	SetStringNN 
(NN 
$strNN -
,NN- .
refreshTokenNN/ ;
)NN; <
;NN< =
PlayerPrefsOO 
.OO 
SaveOO 
(OO 
)OO 
;OO 
}PP 
[RR 
SystemRR 
.RR 
SerializableRR 
]RR 
privateSS 
classSS 
	LoginDataSS 
{TT 
publicUU 
stringUU 
usernameUU 
;UU 
publicVV 
stringVV 
senhaVV 
;VV 
}WW 
}XX Ú
w/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Notifications/Binding/SupressNotificationEditorBinding.cs
	namespace 	
Notifications
 
. 
Binding 
{ 
public 

class -
!SupressNotificationsEditorBinding 2
:3 4(
ISupressNotificationsBinding5 Q
{ 
public 
void #
SupressAllNotifications +
(+ ,
), -
=>. 0
throw1 6
new7 :#
NotImplementedException; R
(R S
)S T
;T U
public		 
void		 *
AskForNotificationPolicyAccess		 2
(		2 3
)		3 4
=>		5 7
throw		8 =
new		> A#
NotImplementedException		B Y
(		Y Z
)		Z [
;		[ \
public 
bool -
!IsNotificationPolicyAccessGranted 5
(5 6
)6 7
=>8 :
throw; @
newA D#
NotImplementedExceptionE \
(\ ]
)] ^
;^ _
} 
} é
^/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/AppRestriction/RestrictedApps.cs
	namespace 	
AppRestriction
 
{ 
public 

class 
RestrictedApps 
{ 
private 
static 

Dictionary !
<! "
string" (
,( )
bool* .
>. /
restrictedApps0 >
=? @
newA D
(D E
)E F
;F G
public

 
static

 
void

 
AddRestrictedApp

 +
(

+ ,
ApplicationInfo

, ;
app

< ?
)

? @
=>

A C
restrictedApps

D R
[

R S
app

S V
.

V W
ProcessName

W b
]

b c
=

d e
true

f j
;

j k
public 
static 
void 
AddRestrictedApp +
(+ ,
string, 2
appProcessName3 A
)A B
=>C E
restrictedAppsF T
[T U
appProcessNameU c
]c d
=e f
trueg k
;k l
public 
static 
void 
AddRestrictedApps ,
(, -
List- 1
<1 2
ApplicationInfo2 A
>A B
appsC G
)G H
{ 	
foreach 
( 
ApplicationInfo $
app% (
in) +
apps, 0
)0 1
{ 
restrictedApps 
[ 
app "
." #
ProcessName# .
]. /
=0 1
true2 6
;6 7
} 
} 	
public 
static 
void 
AddRestrictedApps ,
(, -
List- 1
<1 2
string2 8
>8 9
apps: >
)> ?
{ 	
foreach 
( 
string 
appProcessName *
in+ -
apps. 2
)2 3
{ 
restrictedApps 
[ 
appProcessName -
]- .
=/ 0
true1 5
;5 6
} 
} 	
public 
static 
void 
RemoveRestrictedApp .
(. /
ApplicationInfo/ >
app? B
)B C
=>D F
restrictedAppsG U
[U V
appV Y
.Y Z
ProcessNameZ e
]e f
=g h
falsei n
;n o
public   
static   
void   
RemoveRestrictedApp   .
(  . /
string  / 5
appProcessName  6 D
)  D E
=>  F H
restrictedApps  I W
[  W X
appProcessName  X f
]  f g
=  h i
false  j o
;  o p
public"" 
static"" 

Dictionary""  
<""  !
string""! '
,""' (
bool"") -
>""- .
GetRestrictedApps""/ @
(""@ A
)""A B
=>""C E
restrictedApps""F T
;""T U
}## 
}$$ º
_/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/AppRestriction/IAppRestriction.cs
	namespace 	
AppRestriction
 
{ 
public 

	interface 
IAppRestriction $
{ 
List 
< 
ApplicationInfo 
> 
GetInstalledApps .
(. /
)/ 0
;0 1
List		 
<		 
string		 
>		 
GetRunningAppsNames		 (
(		( )
)		) *
;		* +
}

 
} »
^/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/AppRestriction/AppRestriction.cs
	namespace 	
AppRestriction
 
{ 
public		 

class		 
AppRestriction		 
:		  !
IAppRestriction		" 1
{

 "
IAppRestrictionBinding 
binding &
;& '
public 
Action 
< 
string 
> "
OnRestrictedAppRunning 4
;4 5
public 
AppRestriction 
( 
) 
{ 	
binding 
= 
getNativeBinding &
(& '
)' (
;( )
} 	
public 
List 
< 
ApplicationInfo #
># $
GetInstalledApps% 5
(5 6
)6 7
=>8 :
binding; B
.B C
GetInstalledAppsC S
(S T
)T U
;U V
public 
List 
< 
string 
> 
GetRunningAppsNames /
(/ 0
)0 1
=>2 4
binding5 <
.< =
GetRunningAppsNames= P
(P Q
)Q R
;R S
public 
void '
VerifyRestrictedAppsRunning /
(/ 0
)0 1
{ 	
var 
runningApps 
= 
GetRunningAppsNames 1
(1 2
)2 3
;3 4
var 
restrictedApps 
=  
RestrictedApps! /
./ 0
GetRestrictedApps0 A
(A B
)B C
;C D
foreach 
( 
var 
app 
in 
runningApps  +
)+ ,
{ 
if 
( 
restrictedApps "
." #
ContainsKey# .
(. /
app/ 2
)2 3
&&4 6
restrictedApps7 E
[E F
appF I
]I J
)J K
{   "
OnRestrictedAppRunning!! *
.!!* +
Invoke!!+ 1
(!!1 2
app!!2 5
)!!5 6
;!!6 7
}"" 
}## 
}$$ 	
private&& "
IAppRestrictionBinding&& &
getNativeBinding&&' 7
(&&7 8
)&&8 9
{'' 	"
IAppRestrictionBinding(( "
binding((# *
;((* +
binding** 
=** 
new** 4
(UnsupportedPlatformAppRestrictionBinding** B
(**B C
)**C D
;**D E
return.. 
binding.. 
;.. 
}// 	
}00 
}11  #
\/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Scenes/Login/LoginUIHandler.cs
public 
class 
LoginUIHandler 
: 
MonoBehaviour +
{ 
[ 
Header 
( 
$str 
) 
] 
[ 
SerializeField 
] 
private 
TMP_InputField +
usernameInput, 9
;9 :
[		 
SerializeField		 
]		 
private		 
TMP_InputField		 +
passwordInput		, 9
;		9 :
[

 
SerializeField

 
]

 
private

 
Button

 #
loginButton

$ /
;

/ 0
[ 
SerializeField 
] 
private 
TMP_Text %
	errorText& /
;/ 0
private 
void 
Start 
( 
) 
{ 
if 

( 
usernameInput 
== 
null !
||" $
passwordInput% 2
==3 5
null6 :
||; =
loginButton> I
==J L
nullM Q
||R T
	errorTextU ^
==_ a
nullb f
)f g
{ 	
Debug 
. 
LogError 
( 
$str M
)M N
;N O
enabled 
= 
false 
; 
return 
; 
} 	
passwordInput 
. 
contentType !
=" #
TMP_InputField$ 2
.2 3
ContentType3 >
.> ?
Password? G
;G H
loginButton 
. 
onClick 
. 
AddListener '
(' (
OnLoginClicked( 6
)6 7
;7 8
	errorText 
. 
text 
= 
$str 
; 
} 
public 

void 
OnLoginClicked 
( 
)  
{ 
string 
username 
= 
usernameInput '
.' (
text( ,
., -
Trim- 1
(1 2
)2 3
;3 4
string 
password 
= 
passwordInput '
.' (
text( ,
;, -
if"" 

("" 
string"" 
."" 
IsNullOrEmpty""  
(""  !
username""! )
)"") *
||""+ -
string"". 4
.""4 5
IsNullOrEmpty""5 B
(""B C
password""C K
)""K L
)""L M
{## 	
	errorText$$ 
.$$ 
text$$ 
=$$ 
$str$$ 8
;$$8 9
return%% 
;%% 
}&& 	
if)) 

()) 
AuthManager)) 
.)) 
Instance))  
!=))! #
null))$ (
)))( )
{** 	
	errorText++ 
.++ 
text++ 
=++ 
$str++ 
;++  
AuthManager,, 
.,, 
Instance,,  
.,,  !
HandleLogin,,! ,
(,,, -
username,,- 5
,,,5 6
password,,7 ?
,,,? @
ClearFields,,A L
,,,L M
ShowErrorMessage,,N ^
),,^ _
;,,_ `
}-- 	
else.. 
{// 	
	errorText00 
.00 
text00 
=00 
$str00 =
;00= >
Debug11 
.11 
LogError11 
(11 
$str11 8
)118 9
;119 :
}22 	
}33 
public55 

void55 
ClearFields55 
(55 
)55 
{66 
usernameInput77 
.77 
text77 
=77 
$str77 
;77  
passwordInput88 
.88 
text88 
=88 
$str88 
;88  
	errorText99 
.99 
text99 
=99 
$str99 
;99 
}:: 
public== 

void== 
ShowErrorMessage==  
(==  !
string==! '
message==( /
)==/ 0
{>> 
	errorText?? 
.?? 
text?? 
=?? 
message??  
;??  !
}@@ 
}AA ¢

f/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/AppRestriction/Models/ApplicationInfo.cs
	namespace 	
AppRestriction
 
. 
Models 
{ 
public 

class 
ApplicationInfo  
{ 
public 
string 
Name 
{ 
get  
;  !
}" #
public 
string 
ProcessName !
{" #
get$ '
;' (
}) *
public		 
	Texture2D		 
Icon		 
{		 
get		  #
;		# $
}		% &
public

 
bool

 
isSupressed

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 
ApplicationInfo 
( 
string %
name& *
,* +
string, 2
processName3 >
,> ?
	Texture2D@ I
iconJ N
)N O
{ 	
Name 
= 
name 
; 
ProcessName 
= 
processName %
;% &
Icon 
= 
icon 
; 
} 	
} 
} Â
Z/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Timer/Script/FocusManager.cs
public 
class 
FocusTimerScript 
: 
MonoBehaviour  -
{. /
[ 
SerializeField 
] 
private 
TimerManager )
timerManager* 6
;6 7
[		 
SerializeField		 
]		 
private		 
UIColorManager		 +
colorManager		, 8
;		8 9
[

 
SerializeField

 
]

 
private

 
TMP_Text

 %
timerDisplay

& 2
;

2 3
private 

TimerState 
	lastState  
;  !
private 
void 
Start 
( 
) 
{ 
timerManager 
. 

StartTimer 
(  
)  !
;! "
	lastState 
= 
timerManager  
.  !
CurrentState! -
;- .
colorManager 
. 
UpdateColors !
(! "
timerManager" .
.. /
CurrentState/ ;
); <
;< =
} 
private 
void 
Update 
( 
) 
{ 
timerManager 
. 
UpdateTimer  
(  !
)! "
;" #
UpdateTimerDisplay 
( 
) 
; 
if 

( 
timerManager 
. 
CurrentState %
!=& (
	lastState) 2
)2 3
{4 5
colorManager 
. 
UpdateColors %
(% &
timerManager& 2
.2 3
CurrentState3 ?
)? @
;@ A
	lastState 
= 
timerManager $
.$ %
CurrentState% 1
;1 2
} 	
} 
private 
void 
UpdateTimerDisplay #
(# $
)$ %
{& '
float 

tempoAtual 
= 
timerManager '
.' (
CurrentTime( 3
;3 4
int 
horas 
= 
Mathf 
. 

FloorToInt $
($ %

tempoAtual% /
/0 1
$num2 6
)6 7
;7 8
int 
minutos 
= 
Mathf 
. 

FloorToInt &
(& '
(' (

tempoAtual( 2
%3 4
$num5 9
)9 :
/; <
$num= ?
)? @
;@ A
int   
segundos   
=   
Mathf   
.   

FloorToInt   '
(  ' (

tempoAtual  ( 2
%  3 4
$num  5 7
)  7 8
;  8 9
if"" 

("" 
timerDisplay"" 
)"" 
{"" 
timerDisplay## 
.## 
text## 
=## 
$"##  "
{##" #
horas### (
:##( )
$str##) +
}##+ ,
$str##, -
{##- .
minutos##. 5
:##5 6
$str##6 8
}##8 9
$str##9 :
{##: ;
segundos##; C
:##C D
$str##D F
}##F G
"##G H
;##H I
}$$ 	
}%% 
public'' 

void'' 
OnQuitClicked'' 
('' 
)'' 
{''  !
timerManager(( 
.(( 
Quit(( 
((( 
)(( 
;(( 
colorManager)) 
.)) 
UpdateColors)) !
())! "

TimerState))" ,
.)), -
Idle))- 1
)))1 2
;))2 3
}** 
}++ «
Å/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Notifications/Binding/Android/SupressNotificationsAndroidBinding.cs
	namespace 	
Notifications
 
. 
Binding 
{ 
public 

class .
"SupressNotificationsAndroidBinding 3
:4 5(
ISupressNotificationsBinding6 R
{ 
private 
AndroidJavaObject ! 
supressNotifications" 6
;6 7
private		 
AndroidJavaObject		 !
context		" )
;		) *
public .
"SupressNotificationsAndroidBinding 1
(1 2
)2 3
{ 	
var 
unityJavaClass 
=  
new! $
AndroidJavaClass% 5
(5 6
$str6 V
)V W
;W X
context 
= 
unityJavaClass $
.$ %
	GetStatic% .
<. /
AndroidJavaObject/ @
>@ A
(A B
$strB S
)S T
;T U 
supressNotifications  
=! "
new# &
AndroidJavaObject' 8
(8 9
$str9 h
)h i
;i j
} 	
public 
void #
SupressAllNotifications +
(+ ,
), -
=>. 0 
supressNotifications1 E
.E F

CallStaticF P
(P Q
$strQ j
,j k
contextl s
)s t
;t u
public 
void *
AskForNotificationPolicyAccess 2
(2 3
)3 4
=>5 7 
supressNotifications8 L
.L M

CallStaticM W
(W X
$strX x
,x y
context	z Å
)
Å Ç
;
Ç É
public 
bool -
!IsNotificationPolicyAccessGranted 5
(5 6
)6 7
=>8 : 
supressNotifications; O
.O P

CallStaticP Z
<Z [
bool[ _
>_ `
(` a
$str	a Ñ
,
Ñ Ö
context
Ü ç
)
ç é
;
é è
} 
} ë
c/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Notifications/SupressNotifications.cs
	namespace 	
Notifications
 
{ 
public 

class  
SupressNotifications %
:& '!
ISupressNotifications( =
{ 
private (
ISupressNotificationsBinding ,
binding- 4
;4 5
public		  
SupressNotifications		 #
(		# $
)		$ %
=>		& (
binding		) 0
=		1 2

getBinding		3 =
(		= >
)		> ?
;		? @
public 
void #
SupressAllNotifications +
(+ ,
), -
=>. 0
binding1 8
.8 9#
SupressAllNotifications9 P
(P Q
)Q R
;R S
public 
void *
AskForNotificationPolicyAccess 2
(2 3
)3 4
=>5 7
binding8 ?
.? @*
AskForNotificationPolicyAccess@ ^
(^ _
)_ `
;` a
public 
bool -
!IsNotificationPolicyAccessGranted 5
(5 6
)6 7
=>8 :
binding; B
.B C-
!IsNotificationPolicyAccessGrantedC d
(d e
)e f
;f g
private (
ISupressNotificationsBinding ,

getBinding- 7
(7 8
)8 9
{ 	(
ISupressNotificationsBinding (
binding) 0
;0 1
binding 
= 
new -
!SupressNotificationsEditorBinding ;
(; <
)< =
;= >
return 
binding 
; 
} 	
} 
} ˘
W/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Login/APILoginResponse.cs
[ 
System 
. 
Serializable 
] 
public 
class 
APILoginResponse 
{ 
public 

string 

acessToken 
; 
public 

string 
refreshToken 
; 
public 

string 
error 
; 
} ‚'
\/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Timer/Script/UIColorManager.cs
public 
class 
UIColorManager 
: 
MonoBehaviour +
{, -
[ 
SerializeField 
] 
private 
TMP_Text %
timerDisplay& 2
;2 3
[		 
SerializeField		 
]		 
private		 
TMP_Text		 %
quitButtonText		& 4
;		4 5
[

 
SerializeField

 
]

 
private

 
TMP_Text

 %
modeDisplay

& 1
;

1 2
[ 
SerializeField 
] 
private 
TMP_Text %
	extraText& /
;/ 0
[ 
SerializeField 
] 
private 
Image "
buttonImage# .
;. /
[ 
SerializeField 
] 
private 
Image "
backgroundImage# 2
;2 3
public 

void 
UpdateColors 
( 

TimerState '
state( -
)- .
{/ 0
Color 
corTexto 
, 
corBotao  
,  !
corFundo" *
;* +
string 
modo 
= 
$str 
; 
switch 
( 
state 
) 
{ 
case 

TimerState 
. 
Focus !
:! "
corTexto 
= 
new 
Color32 &
(& '
$num' +
,+ ,
$num- 1
,1 2
$num3 7
,7 8
$num9 =
)= >
;> ?
corBotao 
= 
new 
Color32 &
(& '
$num' +
,+ ,
$num- 1
,1 2
$num3 7
,7 8
$num9 =
)= >
;> ?
corFundo 
= 
new 
Color32 &
(& '
$num' +
,+ ,
$num- 1
,1 2
$num3 7
,7 8
$num9 =
)= >
;> ?
modo 
= 
$str "
;" #
break 
; 
case 

TimerState 
. 
Rest  
:  !
corTexto 
= 
new 
Color32 &
(& '
$num' +
,+ ,
$num- 1
,1 2
$num3 7
,7 8
$num9 =
)= >
;> ?
corBotao 
= 
new 
Color32 &
(& '
$num' +
,+ ,
$num- 1
,1 2
$num3 7
,7 8
$num9 =
)= >
;> ?
corFundo 
= 
new 
Color32 &
(& '
$num' +
,+ ,
$num- 1
,1 2
$num3 7
,7 8
$num9 =
)= >
;> ?
modo 
= 
$str &
;& '
break 
; 
default   
:   
corTexto!! 
=!! 
Color!!  
.!!  !
white!!! &
;!!& '
corBotao"" 
="" 
Color""  
.""  !
gray""! %
;""% &
corFundo## 
=## 
Color##  
.##  !
gray##! %
;##% &
modo$$ 
=$$ 
$str$$ )
;$$) *
break%% 
;%% 
}&& 	
if(( 

((( 
timerDisplay(( 
)(( 
timerDisplay(( &
.((& '
color((' ,
=((- .
corTexto((/ 7
;((7 8
if)) 

()) 
quitButtonText)) 
))) 
quitButtonText)) *
.))* +
color))+ 0
=))1 2
corTexto))3 ;
;)); <
if** 

(** 
modeDisplay** 
)** 
modeDisplay** $
.**$ %
color**% *
=**+ ,
corTexto**- 5
;**5 6
if++ 

(++ 
	extraText++ 
)++ 
	extraText++  
.++  !
color++! &
=++' (
corTexto++) 1
;++1 2
if,, 

(,, 
buttonImage,, 
),, 
buttonImage,, $
.,,$ %
color,,% *
=,,+ ,
corBotao,,- 5
;,,5 6
if-- 

(-- 
backgroundImage-- 
)-- 
backgroundImage-- ,
.--, -
color--- 2
=--3 4
corFundo--5 =
;--= >
if.. 

(.. 
modeDisplay.. 
).. 
modeDisplay.. $
...$ %
text..% )
=..* +
modo.., 0
;..0 1
}// 
}00 í
r/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Notifications/Binding/ISupressNotificationBinding.cs
	namespace 	
Notifications
 
. 
Binding 
{ 
public 

	interface (
ISupressNotificationsBinding 1
{ 
void #
SupressAllNotifications $
($ %
)% &
;& '
void *
AskForNotificationPolicyAccess +
(+ ,
), -
;- .
bool		 -
!IsNotificationPolicyAccessGranted		 .
(		. /
)		/ 0
;		0 1
}

 
} ·
Ä/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/AppRestriction/Bidings/UnsupportedPlatformAppRestrictionBinding.cs
	namespace 	
AppRestriction
 
. 
Bindings !
{ 
public 

class 4
(UnsupportedPlatformAppRestrictionBinding 9
:: ;"
IAppRestrictionBinding< R
{ 
public		 
List		 
<		 
ApplicationInfo		 #
>		# $
GetInstalledApps		% 5
(		5 6
)		6 7
=>		8 :
throw		; @
new		A D#
NotImplementedException		E \
(		\ ]
)		] ^
;		^ _
public

 
List

 
<

 
string

 
>

 
GetRunningAppsNames

 /
(

/ 0
)

0 1
=>

2 4
throw

5 :
new

; >#
NotImplementedException

? V
(

V W
)

W X
;

X Y
} 
} ~
|/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/AppRestriction/Bidings/Android/AndroidAppRestrictionBinding.cs¥5
{/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Resources/AppRestriction/Prefabs/ToggleButton/ToggleButton.cs
[ 
Serializable 
] 
public		 
class		 
ToggleButton		 
:		 
MonoBehaviour		 )
{

 
public 

float 
animationDuration "
=# $
$num% '
;' (
[ 
SerializeField 
] 
private 
AnimationCurve +
	slideEase, 5
=6 7
AnimationCurve8 F
.F G
	EaseInOutG P
(P Q
$numQ R
,R S
$numT U
,U V
$numW X
,X Y
$numZ [
)[ \
;\ ]
public 

Image 

background 
; 
public 

Sprite 
enableBackground "
;" #
public 

Sprite 
disableBackground #
;# $
public 

Button 
button 
; 
public 

TMP_Text 

buttonText 
; 
	protected 
float 
value 
; 
public 


GameObject 
handle 
; 
public 

float 
leftX 
= 
$num 
; 
public 

float 
rightX 
= 
$num 
; 
public 

Action 
	OnDisable 
; 
public 

Action 
OnEnable 
; 
public 

void 
Toggle 
( 
) 
{   
var!! 
	nextValue!! 
=!! 
value!! 
==!!  
$num!!! "
?!!# $
$num!!% &
:!!' (
$num!!) *
;!!* +
if"" 

("" 
	nextValue"" 
=="" 
$num"" 
)"" 
{## 	
	OnDisable$$ 
.$$ 
Invoke$$ 
($$ 
)$$ 
;$$ 
}%% 	
else&& 
{'' 	
OnEnable(( 
.(( 
Invoke(( 
((( 
)(( 
;(( 
})) 	

setVisuals** 
(** 
	nextValue** 
)** 
;** 
StartCoroutine++ 
(++ 
AnimateSlider++ $
(++$ %
	nextValue++% .
)++. /
)++/ 0
;++0 1
},, 
private.. 
IEnumerator.. 
AnimateSlider.. %
(..% &
float..& +
	nextValue.., 5
)..5 6
{// 	
button00 
.00 
interactable00 
=00  !
false00" '
;00' (
float11 

startValue11 
=11 
value11 $
;11$ %
float22 
endValue22 
=22 
	nextValue22 &
;22& '
float44 
time44 
=44 
$num44 
;44 
if55 
(55 
animationDuration55 !
>55" #
$num55$ %
)55% &
{66 
while77 
(77 
time77 
<77 
animationDuration77 /
)77/ 0
{88 
time99 
+=99 
Time99  
.99  !
	deltaTime99! *
;99* +
float;; 

lerpFactor;; $
=;;% &
	slideEase;;' 0
.;;0 1
Evaluate;;1 9
(;;9 :
time;;: >
/;;? @
animationDuration;;A R
);;R S
;;;S T
value<< 
=<< 
Mathf<< !
.<<! "
Lerp<<" &
(<<& '

startValue<<' 1
,<<1 2
endValue<<3 ;
,<<; <

lerpFactor<<= G
)<<G H
;<<H I

moveHandle>> 
(>> 
	nextValue>> (
,>>( )

lerpFactor>>* 4
)>>4 5
;>>5 6
yield@@ 
return@@  
null@@! %
;@@% &
}AA 
}BB 
valueDD 
=DD 
endValueDD 
;DD 
buttonEE 
.EE 
interactableEE 
=EE  !
trueEE" &
;EE& '
}FF 	
privateHH 
voidHH 

moveHandleHH 
(HH 
floatHH !
	nextValueHH" +
,HH+ ,
floatHH- 2

lerpFactorHH3 =
)HH= >
{II 
ifJJ 

(JJ 
	nextValueJJ 
==JJ 
$numJJ 
)JJ 
{KK 	
handleLL 
.LL 
	transformLL 
.LL 
localPositionLL *
=LL+ ,
newLL- 0
Vector3LL1 8
(LL8 9
MathfLL9 >
.LL> ?
LerpLL? C
(LLC D
leftXLLD I
,LLI J
rightXLLK Q
,LLQ R

lerpFactorLLS ]
)LL] ^
,LL^ _
handleLL` f
.LLf g
	transformLLg p
.LLp q
localPositionLLq ~
.LL~ 
y	LL Ä
,
LLÄ Å
handle
LLÇ à
.
LLà â
	transform
LLâ í
.
LLí ì
localPosition
LLì †
.
LL† °
z
LL° ¢
)
LL¢ £
;
LL£ §
}MM 	
elseNN 
{OO 	
handlePP 
.PP 
	transformPP 
.PP 
localPositionPP *
=PP+ ,
newPP- 0
Vector3PP1 8
(PP8 9
MathfPP9 >
.PP> ?
LerpPP? C
(PPC D
rightXPPD J
,PPJ K
leftXPPL Q
,PPQ R

lerpFactorPPS ]
)PP] ^
,PP^ _
handlePP` f
.PPf g
	transformPPg p
.PPp q
localPositionPPq ~
.PP~ 
y	PP Ä
,
PPÄ Å
handle
PPÇ à
.
PPà â
	transform
PPâ í
.
PPí ì
localPosition
PPì †
.
PP† °
z
PP° ¢
)
PP¢ £
;
PP£ §
}QQ 	
}RR 
privateTT 
voidTT 

setVisualsTT 
(TT 
floatTT !
	nextValueTT" +
)TT+ ,
{UU 

buttonTextVV 
.VV 
textVV 
=VV 
	nextValueVV #
==VV$ &
$numVV' (
?VV) *
$strVV+ 0
:VV1 2
$strVV3 7
;VV7 8

backgroundWW 
.WW 
spriteWW 
=WW 
	nextValueWW %
==WW& (
$numWW) *
?WW+ ,
disableBackgroundWW. ?
:WW@ A
enableBackgroundWWB R
;WWR S
}XX 
}YY ©2
Z/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Timer/Script/TimerManager.cs
public 
enum 

TimerState 
{ 
Focus 
, 
Rest  $
,$ %
Idle& *
}+ ,
public 
class 
TimerManager 
: 
MonoBehaviour )
{* +
[		 
SerializeField		 
]		 
private		 
float		 "
	focusTime		# ,
=		- .
$num		/ 4
;		4 5
[

 
SerializeField

 
]

 
private

 
float

 "
restTime

# +
=

, -
$num

. 2
;

2 3
[ 
SerializeField 
] 
private 
float "
longRestTime# /
=0 1
$num2 7
;7 8
[ 
SerializeField 
] 
private 
int  
totalSessions! .
=/ 0
$num1 2
;2 3
public 


TimerState 
CurrentState "
{# $
get% (
;( )
private* 1
set2 5
;5 6
}7 8
=9 :

TimerState; E
.E F
FocusF K
;K L
public 

float 
CurrentTime 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
private 
int 
focusSessionsCount "
=# $
$num% &
;& '
private 
float 
currentRestDuration %
=& '
$num( *
;* +
private 
int 
completedSessions !
=" #
$num$ %
;% &
private 
float 
totalFocusTime  
=! "
$num# %
;% &
private 
float 
totalRestTime 
=  !
$num" $
;$ %
public 

void 

StartTimer 
( 
) 
{ 
CurrentState 
= 

TimerState !
.! "
Focus" '
;' (
CurrentTime 
= 
	focusTime 
;  
} 
public 

void 
UpdateTimer 
( 
) 
{ 
if 

( 
CurrentState 
== 

TimerState &
.& '
Idle' +
)+ ,
return- 3
;3 4
CurrentTime 
-= 
Time 
.  
	deltaTime  )
;) *
if 

( 
CurrentTime 
<= 
$num 
) 
{ 
if   
(   
CurrentState   
==   

TimerState    *
.  * +
Focus  + 0
)  0 1
{  2 3
totalFocusTime!! 
+=!! !
	focusTime!!" +
;!!+ ,
focusSessionsCount"" "
++""" $
;""$ %
if## 
(## 
focusSessionsCount## &
%##' (
$num##) *
==##+ -
$num##. /
)##/ 0
{##1 2
StartLongRest$$ !
($$! "
)$$" #
;$$# $
}%% 
else%% 
{%% 
	StartRest&& 
(&& 
)&& 
;&&  
}'' 
}(( 
else(( 
{(( 
totalRestTime)) 
+=))  
currentRestDuration))! 4
;))4 5
completedSessions** !
++**! #
;**# $
if++ 
(++ 
completedSessions++ %
>=++& (
totalSessions++) 6
)++6 7
{++8 9

EndSession,, 
(,, 
),,  
;,,  !
}-- 
else-- 
{-- 

StartFocus.. 
(.. 
)..  
;..  !
}// 
}00 
}11 	
}22 
public44 

void44 
Quit44 
(44 
)44 
{44 
if55 

(55 
CurrentState55 
==55 

TimerState55 &
.55& '
Focus55' ,
)55, -
{55. /
totalFocusTime66 
+=66 
	focusTime66 '
-66( )
CurrentTime66* 5
;665 6
}77 	
else77	 
if77 
(77 
CurrentState77 
==77 !

TimerState77" ,
.77, -
Rest77- 1
)771 2
{772 3
totalRestTime88 
+=88 
currentRestDuration88 0
-881 2
CurrentTime883 >
;88> ?
}99 	

EndSession:: 
(:: 
):: 
;:: 
};; 
private== 
void== 

StartFocus== 
(== 
)== 
{== 
CurrentState>> 
=>> 

TimerState>> !
.>>! "
Focus>>" '
;>>' (
CurrentTime?? 
=?? 
	focusTime?? 
;??  
}@@ 
privateAA 
voidAA 
	StartRestAA 
(AA 
)AA 
{AA 
CurrentStateBB 
=BB 

TimerStateBB !
.BB! "
RestBB" &
;BB& '
currentRestDurationCC 
=CC 
restTimeCC &
;CC& '
CurrentTimeDD 
=DD 
restTimeDD 
;DD 
}EE 
privateGG 
voidGG 
StartLongRestGG 
(GG 
)GG  
{GG  !
CurrentStateHH 
=HH 

TimerStateHH !
.HH! "
RestHH" &
;HH& '
currentRestDurationII 
=II 
longRestTimeII *
;II* +
CurrentTimeJJ 
=JJ 
longRestTimeJJ "
;JJ" #
}KK 
privateLL 
voidLL 

EndSessionLL 
(LL 
)LL 
{LL 
CurrentStateMM 
=MM 

TimerStateMM !
.MM! "
IdleMM" &
;MM& '
CurrentTimeNN 
=NN 
$numNN 
;NN 
}OO 
}PP Ê%
j/Users/samuel.matos/Documents/studies/es/Focus-Library/Assets/Scenes/AppRestriction/AppRestrictionScene.cs
public

 
class

 
AppRestrictionScene

  
:

! "
MonoBehaviour

# 0
{ 
[ 
SerializeField 
] 

GameObject 
appsContent  +
;+ ,
[ 
SerializeField 
] 
ToggleButton ! 
appRestrictionPrefab" 6
;6 7
[ 
SerializeField 
] 
float 
verifyInterval )
=* +
$num, .
;. /
private 
AppRestriction 
. 
AppRestriction )
appRestriction* 8
;8 9
void 
Start	 
( 
) 
{ 
appRestriction 
= 
new 
AppRestriction +
.+ ,
AppRestriction, :
(: ;
); <
;< =
var 
apps 
= 
appRestriction !
.! "
GetInstalledApps" 2
(2 3
)3 4
;4 5
setupAppsContent 
( 
apps 
) 
; 
appRestriction 
. "
OnRestrictedAppRunning -
+=. 0
(1 2
app2 5
)5 6
=>7 9
Debug: ?
.? @
Log@ C
(C D
$strD ^
+_ `
appa d
)d e
;e f
} 
void 
setupAppsContent	 
( 
List 
< 
ApplicationInfo .
>. /
apps0 4
)4 5
{ 
foreach 
( 
var 
app 
in 
apps  
)  !
{ 	
var 
	appObject 
= 
Instantiate '
(' ( 
appRestrictionPrefab( <
)< =
;= >
	appObject   
.   
	transform   
.    
	SetParent    )
(  ) *
appsContent  * 5
.  5 6
	transform  6 ?
,  ? @
false  A F
)  F G
;  G H
	appObject"" 
."" "
GetComponentInChildren"" ,
<"", -
Image""- 2
>""2 3
(""3 4
)""4 5
.""5 6
sprite""6 <
=""= >
Sprite""? E
.""E F
Create""F L
(""L M
app""M P
.""P Q
Icon""Q U
,""U V
new""W Z
Rect""[ _
(""_ `
$num""` a
,""a b
$num""c d
,""d e
app""f i
.""i j
Icon""j n
.""n o
width""o t
,""t u
app""v y
.""y z
Icon""z ~
.""~ 
height	"" Ö
)
""Ö Ü
,
""Ü á
new
""à ã
Vector2
""å ì
(
""ì î
$num
""î ò
,
""ò ô
$num
""ö û
)
""û ü
)
""ü †
;
""† °
	appObject$$ 
.$$ 
OnEnable$$ 
+=$$ !
($$" #
)$$# $
=>$$% '
{$$( )
AppRestriction$$* 8
.$$8 9
RestrictedApps$$9 G
.$$G H
AddRestrictedApp$$H X
($$X Y
app$$Y \
)$$\ ]
;$$] ^
}$$_ `
;$$` a
	appObject%% 
.%% 
	OnDisable%% 
+=%%  "
(%%# $
)%%$ %
=>%%& (
{%%) *
AppRestriction%%+ 9
.%%9 :
RestrictedApps%%: H
.%%H I
RemoveRestrictedApp%%I \
(%%\ ]
app%%] `
)%%` a
;%%a b
}%%c d
;%%d e
}&& 	
}'' 
public)) 

void)) 
StartVerifying)) 
()) 
)))  
=>))! #
StartCoroutine))$ 2
())2 3
VerifyEverySeconds))3 E
())E F
verifyInterval))F T
)))T U
)))U V
;))V W
IEnumerator++ 
VerifyEverySeconds++ "
(++" #
float++# (
seconds++) 0
)++0 1
{,, 
Application-- 
.-- 
runInBackground-- #
=--$ %
true--& *
;--* +
while.. 
(.. 
true.. 
).. 
{// 	
yield00 
return00 
new00 
WaitForSeconds00 +
(00+ ,
seconds00, 3
)003 4
;004 5
appRestriction22 
.22 '
VerifyRestrictedAppsRunning22 6
(226 7
)227 8
;228 9
}33 	
}44 
}55 