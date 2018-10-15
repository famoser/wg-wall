ƒ
IC:\Users\flmo\source\repos\wg-wall\WgWall\Api\Dto\Base\BaseIdEntityDto.cs
	namespace 	
WgWall
 
. 
Dto 
. 
Base 
{ 
public 

class 
BaseIdEntityDto  
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
} 
} à
DC:\Users\flmo\source\repos\wg-wall\WgWall\Api\Dto\FrontendUserDto.cs
	namespace 	
WgWall
 
. 
Dto 
{ 
public		 

class		 
FrontendUserDto		  
:		! "
BaseIdEntityDto		# 2
{

 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
Karma 
{ 
get 
; 
set  #
;# $
}% &
public 
string 
ProfileImageSrc %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} Ü
?C:\Users\flmo\source\repos\wg-wall\WgWall\Api\Dto\ProductDto.cs
	namespace 	
WgWall
 
. 
Dto 
{ 
public		 

class		 

ProductDto		 
:		 
BaseIdEntityDto		 -
{

 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
Amount 
{ 
get 
;  
set! $
;$ %
}& '
public 
int 
? 

BoughtById 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ±
?C:\Users\flmo\source\repos\wg-wall\WgWall\Api\Dto\SettingDto.cs
	namespace 	
WgWall
 
. 
Dto 
{ 
public		 

class		 

SettingDto		 
{

 
public 
string 
Key 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 
Value 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ˛
PC:\Users\flmo\source\repos\wg-wall\WgWall\Api\Request\Base\AccountablePayload.cs
	namespace 	
WgWall
 
. 
Api 
. 
Request 
. 
Base !
{ 
public 

class 
AccountablePayload #
{		 
public

 
int

 
FrontendUserId

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
} 
} ”
LC:\Users\flmo\source\repos\wg-wall\WgWall\Api\Request\FrontendUserPayload.cs
	namespace 	
WgWall
 
. 
Api 
. 
Request 
{ 
public 

class 
FrontendUserPayload $
{		 
public

 
string

 
Name

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
} 
} Ä
KC:\Users\flmo\source\repos\wg-wall\WgWall\Api\Request\ProductPostPayload.cs
	namespace 	
WgWall
 
. 
Api 
. 
Request 
{ 
public		 

class		 
ProductPostPayload		 #
:		$ %
AccountablePayload		& 8
{

 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ˇ
JC:\Users\flmo\source\repos\wg-wall\WgWall\Api\Request\ProductPutPayload.cs
	namespace 	
WgWall
 
. 
Api 
. 
Request 
{ 
public		 

class		 
ProductPutPayload		 "
{

 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
Amount 
{ 
get 
;  
set! $
;$ %
}& '
public 
int 
BoughtBy 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ·
GC:\Users\flmo\source\repos\wg-wall\WgWall\Api\Request\SettingPayload.cs
	namespace 	
WgWall
 
. 
Api 
. 
Request 
{ 
public 

class 
SettingPayload 
{		 
public

 
string

 
Key

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
public 
string 
Value 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ·
OC:\Users\flmo\source\repos\wg-wall\WgWall\Controllers\FrontendUserController.cs
	namespace 	
WgWall
 
. 
Controllers 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class "
FrontendUserController '
:( )
ControllerBase* 8
{ 
private 
readonly #
IFrontendUserRepository 0#
_frontendUserRepository1 H
;H I
private 
readonly 
IMapper  
_mapper! (
;( )
public "
FrontendUserController %
(% &#
IFrontendUserRepository& ="
frontendUserRepository> T
)T U
{ 	#
_frontendUserRepository #
=$ %"
frontendUserRepository& <
;< =
var 
config 
= 
new 
MapperConfiguration 0
(0 1
cfg1 4
=>5 7
cfg8 ;
.; <
	CreateMap< E
<E F
FrontendUserF R
,R S
FrontendUserDtoT c
>c d
(d e
)e f
)f g
;g h
_mapper 
= 
new 
Mapper  
(  !
config! '
)' (
;( )
} 	
[   	
HttpGet  	 
]   
public!! 
async!! 
Task!! 
<!! 
IActionResult!! '
>!!' (
GetFrontendUsers!!) 9
(!!9 :
)!!: ;
{"" 	
var## 
users## 
=## 
await## #
_frontendUserRepository## 5
.##5 6
GetAllAsync##6 A
(##A B
)##B C
;##C D
var%% 
usersDto%% 
=%% 
_mapper%% "
.%%" #
Map%%# &
<%%& '
IList%%' ,
<%%, -
FrontendUserDto%%- <
>%%< =
>%%= >
(%%> ?
users%%? D
)%%D E
;%%E F
return&& 
Ok&& 
(&& 
usersDto&& 
)&& 
;&&  
}'' 	
[)) 	
HttpPost))	 
])) 
public** 
async** 
Task** 
<** 
IActionResult** '
>**' (
PostFrontendUser**) 9
(**9 :
[**: ;
FromBody**; C
]**C D
FrontendUserPayload**E X
user**Y ]
)**] ^
{++ 	
if,, 
(,, 
!,, 

ModelState,, 
.,, 
IsValid,, #
),,# $
{-- 
return.. 

BadRequest.. !
(..! "

ModelState.." ,
).., -
;..- .
}// 
var11 
newUser11 
=11 
await11 #
_frontendUserRepository11  7
.117 8
CreateAsync118 C
(11C D
user11D H
.11H I
Name11I M
)11M N
;11N O
var22 

newUserDto22 
=22 
_mapper22 $
.22$ %
Map22% (
<22( )
FrontendUserDto22) 8
>228 9
(229 :
newUser22: A
)22A B
;22B C
return33 
Ok33 
(33 

newUserDto33  
)33  !
;33! "
}44 	
}55 
}66 ”/
JC:\Users\flmo\source\repos\wg-wall\WgWall\Controllers\ProductController.cs
	namespace 	
WgWall
 
. 
Controllers 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class 
ProductController "
:# $
ControllerBase% 3
{ 
private 
readonly 
IProductRepository +
_productRepository, >
;> ?
private 
readonly #
IFrontendUserRepository 0#
_frontendUserRepository1 H
;H I
private 
readonly 
IMapper  
_mapper! (
;( )
public 
ProductController  
(  !
IProductRepository! 3
productRepository4 E
,E F#
IFrontendUserRepositoryG ^"
frontendUserRepository_ u
)u v
{ 	
_productRepository 
=  
productRepository! 2
;2 3#
_frontendUserRepository #
=$ %"
frontendUserRepository& <
;< =
var 
config 
= 
new 
MapperConfiguration 0
(0 1
cfg1 4
=>5 7
cfg8 ;
.; <
	CreateMap< E
<E F
ProductF M
,M N

ProductDtoO Y
>Y Z
(Z [
)[ \
)\ ]
;] ^
_mapper 
= 
new 
Mapper  
(  !
config! '
)' (
;( )
}   	
[## 	
HttpGet##	 
]## 
public$$ 
async$$ 
Task$$ 
<$$ 
IActionResult$$ '
>$$' (
GetProducts$$) 4
($$4 5
)$$5 6
{%% 	
var&& 
products&& 
=&& 
await&&  
_productRepository&&! 3
.&&3 4
GetAllAsync&&4 ?
(&&? @
)&&@ A
;&&A B
var(( 
productsDto(( 
=(( 
_mapper(( %
.((% &
Map((& )
<(() *
IList((* /
<((/ 0

ProductDto((0 :
>((: ;
>((; <
(((< =
products((= E
)((E F
;((F G
return)) 
Ok)) 
()) 
productsDto)) !
)))! "
;))" #
}** 	
[-- 	
HttpPut--	 
(-- 
$str-- 
)-- 
]-- 
public.. 
async.. 
Task.. 
<.. 
IActionResult.. '
>..' (

PutProduct..) 3
(..3 4
[..4 5
	FromRoute..5 >
]..> ?
int..@ C
id..D F
,..F G
[..H I
FromBody..I Q
]..Q R
ProductPutPayload..S d
payload..e l
)..l m
{// 	
if00 
(00 
!00 

ModelState00 
.00 
IsValid00 #
)00# $
{11 
return22 

BadRequest22 !
(22! "

ModelState22" ,
)22, -
;22- .
}33 
var55 
product55 
=55 
await55 
_productRepository55  2
.552 3
TryGet553 9
(559 :
id55: <
)55< =
;55= >
product66 
.66 
Name66 
=66 
payload66 "
.66" #
Name66# '
;66' (
product77 
.77 
Amount77 
=77 
payload77 $
.77$ %
Amount77% +
;77+ ,
product88 
.88 
BoughtBy88 
=88 
await88 $#
_frontendUserRepository88% <
.88< =
TryGet88= C
(88C D
payload88D K
.88K L
BoughtBy88L T
)88T U
;88U V
product99 
.99 

BoughtById99 
=99  
product99! (
.99( )
BoughtBy99) 1
?991 2
.992 3
Id993 5
;995 6
await;; 
_productRepository;; $
.;;$ %
Update;;% +
(;;+ ,
product;;, 3
);;3 4
;;;4 5
return== 
	NoContent== 
(== 
)== 
;== 
}>> 	
[AA 	
HttpPostAA	 
]AA 
publicBB 
asyncBB 
TaskBB 
<BB 
IActionResultBB '
>BB' (
PostProductBB) 4
(BB4 5
[BB5 6
FromBodyBB6 >
]BB> ?
ProductPostPayloadBB@ R
payloadBBS Z
)BBZ [
{CC 	
ifDD 
(DD 
!DD 

ModelStateDD 
.DD 
IsValidDD #
||DD$ &
payloadDD' .
.DD. /
NameDD/ 3
.DD3 4
IsNullOrEmptyDD4 A
(DDA B
)DDB C
)DDC D
{EE 
returnFF 

BadRequestFF !
(FF! "

ModelStateFF" ,
)FF, -
;FF- .
}GG 
varJJ 
productJJ 
=JJ 
awaitJJ 
_productRepositoryJJ  2
.JJ2 3
CreateJJ3 9
(JJ9 :
payloadJJ: A
.JJA B
NameJJB F
,JJF G
awaitJJH M#
_frontendUserRepositoryJJN e
.JJe f
TryGetJJf l
(JJl m
payloadJJm t
.JJt u
FrontendUserId	JJu É
)
JJÉ Ñ
)
JJÑ Ö
;
JJÖ Ü
varKK 

productDtoKK 
=KK 
_mapperKK $
.KK$ %
MapKK% (
<KK( )

ProductDtoKK) 3
>KK3 4
(KK4 5
productKK5 <
)KK< =
;KK= >
returnLL 
OkLL 
(LL 

productDtoLL  
)LL  !
;LL! "
}MM 	
}NN 
}OO †
MC:\Users\flmo\source\repos\wg-wall\WgWall\Controllers\SampleDataController.cs
	namespace 	
WgWall
 
. 
Controllers 
{ 
[		 
Route		 

(		
 
$str		 
)		 
]		 
public

 

class

  
SampleDataController

 %
:

& '

Controller

( 2
{ 
private 
static 
string 
[ 
] 
	Summaries  )
=* +
new, /
[/ 0
]0 1
{ 	
$str 
, 
$str !
,! "
$str# +
,+ ,
$str- 3
,3 4
$str5 ;
,; <
$str= C
,C D
$strE L
,L M
$strN S
,S T
$strU a
,a b
$strc n
} 	
;	 

[ 	
HttpGet	 
( 
$str 
) 
] 
public 
IEnumerable 
< 
WeatherForecast *
>* +
WeatherForecasts, <
(< =
)= >
{ 	
var 
rng 
= 
new 
Random  
(  !
)! "
;" #
return 

Enumerable 
. 
Range #
(# $
$num$ %
,% &
$num' (
)( )
.) *
Select* 0
(0 1
index1 6
=>7 9
new: =
WeatherForecast> M
{ 
DateFormatted 
= 
DateTime  (
.( )
Now) ,
., -
AddDays- 4
(4 5
index5 :
): ;
.; <
ToString< D
(D E
$strE H
)H I
,I J
TemperatureC 
= 
rng "
." #
Next# '
(' (
-( )
$num) +
,+ ,
$num- /
)/ 0
,0 1
Summary 
= 
	Summaries #
[# $
rng$ '
.' (
Next( ,
(, -
	Summaries- 6
.6 7
Length7 =
)= >
]> ?
} 
) 
; 
} 	
public 
class 
WeatherForecast $
{ 	
public 
string 
DateFormatted '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public   
int   
TemperatureC   #
{  $ %
get  & )
;  ) *
set  + .
;  . /
}  0 1
public!! 
string!! 
Summary!! !
{!!" #
get!!$ '
;!!' (
set!!) ,
;!!, -
}!!. /
public## 
int## 
TemperatureF## #
{$$ 
get%% 
{&& 
return'' 
$num'' 
+'' 
(''  !
int''! $
)''$ %
(''% &
TemperatureC''& 2
/''3 4
$num''5 ;
)''; <
;''< =
}(( 
})) 
}** 	
}++ 
},, ß
JC:\Users\flmo\source\repos\wg-wall\WgWall\Controllers\SettingController.cs
	namespace 	
WgWall
 
. 
Controllers 
{ 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class 
SettingController "
:# $
ControllerBase% 3
{ 
private 
readonly 
ISettingRepository +
_settingRepository, >
;> ?
private 
readonly 
IMapper  
_mapper! (
;( )
public 
SettingController  
(  !
ISettingRepository! 3
settingRepository4 E
)E F
{ 	
_settingRepository 
=  
settingRepository! 2
;2 3
var 
config 
= 
new 
MapperConfiguration 0
(0 1
cfg1 4
=>5 7
cfg8 ;
.; <
	CreateMap< E
<E F
SettingF M
,M N

SettingDtoO Y
>Y Z
(Z [
)[ \
)\ ]
;] ^
_mapper 
= 
new 
Mapper  
(  !
config! '
)' (
;( )
} 	
[   	
HttpGet  	 
]   
public!! 
async!! 
Task!! 
<!! 
IActionResult!! '
>!!' (
Get!!) ,
(!!, -
)!!- .
{"" 	
var## 
settings## 
=## 
await##  
_settingRepository##! 3
.##3 4
GetAllAsync##4 ?
(##? @
)##@ A
;##A B
var%% 
settingsDto%% 
=%% 
_mapper%% %
.%%% &
Map%%& )
<%%) *
IList%%* /
<%%/ 0

SettingDto%%0 :
>%%: ;
>%%; <
(%%< =
settings%%= E
)%%E F
;%%F G
return&& 
Ok&& 
(&& 
settingsDto&& !
)&&! "
;&&" #
}'' 	
[** 	
HttpPost**	 
]** 
public++ 
async++ 
Task++ 
<++ 
IActionResult++ '
>++' (
PostSetting++) 4
(++4 5
[++5 6
FromBody++6 >
]++> ?
SettingPayload++@ N
payload++O V
)++V W
{,, 	
if-- 
(-- 
!-- 

ModelState-- 
.-- 
IsValid-- #
)--# $
{.. 
return// 

BadRequest// !
(//! "

ModelState//" ,
)//, -
;//- .
}00 
await22 
_settingRepository22 $
.22$ %
Persist22% ,
(22, -
payload22- 4
.224 5
Key225 8
,228 9
payload22: A
.22A B
Value22B G
)22G H
;22H I
return44 
	NoContent44 
(44 
)44 
;44 
}55 	
}66 
}77 ⁄

?C:\Users\flmo\source\repos\wg-wall\WgWall\Pages\Error.cshtml.cs
	namespace		 	
WgWall		
 
.		 
Pages		 
{

 
[ 
ResponseCache 
( 
Duration 
= 
$num 
,  
Location! )
=* +!
ResponseCacheLocation, A
.A B
NoneB F
,F G
NoStoreH O
=P Q
trueR V
)V W
]W X
public 

class 

ErrorModel 
: 
	PageModel '
{ 
public 
string 
	RequestId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
bool 
ShowRequestId !
=>" $
!% &
string& ,
., -
IsNullOrEmpty- :
(: ;
	RequestId; D
)D E
;E F
public 
void 
OnGet 
( 
) 
{ 	
	RequestId 
= 
Activity  
.  !
Current! (
?( )
.) *
Id* ,
??- /
HttpContext0 ;
.; <
TraceIdentifier< K
;K L
} 	
} 
} ‘
4C:\Users\flmo\source\repos\wg-wall\WgWall\Program.cs
	namespace 	
WgWall
 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	 
CreateWebHostBuilder  
(  !
args! %
)% &
.& '
Build' ,
(, -
)- .
.. /
Run/ 2
(2 3
)3 4
;4 5
} 	
public 
static 
IWebHostBuilder % 
CreateWebHostBuilder& :
(: ;
string; A
[A B
]B C
argsD H
)H I
=>J L
WebHost 
.  
CreateDefaultBuilder (
(( )
args) -
)- .
. 

UseStartup 
< 
Startup #
># $
($ %
)% &
;& '
} 
} ¿%
4C:\Users\flmo\source\repos\wg-wall\WgWall\Startup.cs
	namespace 	
WgWall
 
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
virtual 
void 
ConfigureServices -
(- .
IServiceCollection. @
servicesA I
)I J
{ 	
services 
. 
AddMvc 
( 
) 
. #
SetCompatibilityVersion 5
(5 6 
CompatibilityVersion6 J
.J K
Version_2_1K V
)V W
;W X
services 
. 
AddSpaStaticFiles &
(& '
configuration' 4
=>5 7
{ 
configuration   
.   
RootPath   &
=  ' (
$str  ) 9
;  9 :
}!! 
)!! 
;!! 
var## 

connection## 
=## 
$str## 7
;##7 8
services$$ 
.$$ 
AddDbContext$$ !
<$$! "
MyDbContext$$" -
>$$- .
($$. /
options$$/ 6
=>$$7 9
options$$: A
.$$A B!
UseLazyLoadingProxies$$B W
($$W X
)$$X Y
.$$Y Z
	UseSqlite$$Z c
($$c d

connection$$d n
,$$n o
x$$p q
=>$$r t
x$$u v
.$$v w
MigrationsAssembly	$$w â
(
$$â ä
$str
$$ä ù
)
$$ù û
)
$$û ü
)
$$ü †
;
$$† °
services%% 
.%% 
	AddScoped%% 
<%% #
IFrontendUserRepository%% 6
,%%6 7"
FrontendUserRepository%%8 N
>%%N O
(%%O P
)%%P Q
;%%Q R
services&& 
.&& 
	AddScoped&& 
<&& 
IProductRepository&& 1
,&&1 2
ProductRepository&&3 D
>&&D E
(&&E F
)&&F G
;&&G H
services'' 
.'' 
	AddScoped'' 
<'' 
ISettingRepository'' 1
,''1 2
SettingRepository''3 D
>''D E
(''E F
)''F G
;''G H
}(( 	
public++ 
virtual++ 
void++ 
	Configure++ %
(++% &
IApplicationBuilder++& 9
app++: =
,++= >
IHostingEnvironment++? R
env++S V
)++V W
{,, 	
if-- 
(-- 
env-- 
.-- 
IsDevelopment-- !
(--! "
)--" #
)--# $
{.. 
app// 
.// %
UseDeveloperExceptionPage// -
(//- .
)//. /
;/// 0
}00 
else11 
{22 
app33 
.33 
UseExceptionHandler33 '
(33' (
$str33( 0
)330 1
;331 2
app44 
.44 
UseHsts44 
(44 
)44 
;44 
}55 
app77 
.77 
UseHttpsRedirection77 #
(77# $
)77$ %
;77% &
app88 
.88 
UseStaticFiles88 
(88 
)88  
;88  !
app99 
.99 
UseSpaStaticFiles99 !
(99! "
)99" #
;99# $
app;; 
.;; 
UseMvc;; 
(;; 
routes;; 
=>;;  
{<< 
routes== 
.== 
MapRoute== 
(==  
name>> 
:>> 
$str>> #
,>># $
template?? 
:?? 
$str?? A
)??A B
;??B C
}@@ 
)@@ 
;@@ 
appBB 
.BB 
UseSpaBB 
(BB 
spaBB 
=>BB 
{CC 
spaGG 
.GG 
OptionsGG 
.GG 

SourcePathGG &
=GG' (
$strGG) 4
;GG4 5
ifII 
(II 
envII 
.II 
IsDevelopmentII %
(II% &
)II& '
)II' (
{JJ 
spaKK 
.KK 
UseAngularCliServerKK +
(KK+ ,
	npmScriptKK, 5
:KK5 6
$strKK7 >
)KK> ?
;KK? @
}LL 
}MM 
)MM 
;MM 
}NN 	
}OO 
}PP 