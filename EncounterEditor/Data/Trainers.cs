﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Data
{
    public static class Trainers
    {
		public static List<string> nameList = new List<string>()
		{
			"--",
			"Elena",
			"Dan",
			"Aspen",
			"Bianca",
			"N",
			"N",
			"Matthew",
			"Bobby",
			"John",
			"Trisha",
			"Lou",
			"Olwen",
			"Tony",
			"Roberto",
			"Fletcher",
			"Abe",
			"Kevin",
			"Devon",
			"Chaz",
			"Fredric",
			"Hawk & Dar",
			"Joe & Ross",
			"Masa & Yas",
			"Kay & Ali",
			"Ami & Eira",
			"Bob",
			"Alain",
			"Heidi",
			"Alex",
			"Connor",
			"Mariana",
			"Henry",
			"Sibyl",
			"Brad",
			"Elizandra",
			"Cassandra",
			"Robert",
			"Shauntal",
			"Marshal",
			"Grimsley",
			"Caitlin",
			"Shaun",
			"Lucca",
			"Shel",
			"Austin",
			"Martin",
			"Persephone",
			"Mari",
			"Elena",
			"Elena",
			"Elena",
			"Aspen",
			"Todd",
			"Alex",
			"Alex",
			"Alex",
			"Connor",
			"Marco",
			"Tony",
			"Tony",
			"Tony",
			"Roberto",
			"Jonah",
			"Dan",
			"Dan",
			"Dan",
			"Bob",
			"Lamarcus",
			"Bobby",
			"Bobby",
			"Bobby",
			"John",
			"Hawk & Dar",
			"Hawk & Dar",
			"Joe & Ross",
			"Joe & Ross",
			"Masa & Yas",
			"Masa & Yas",
			"Les & Web",
			"Alf & Fred",
			"Kay & Ali",
			"Kay & Ali",
			"Ami & Eira",
			"Ami & Eira",
			"Cam & Abby",
			"Ai & Ciel",
			"Kat & Phae",
			"Dana",
			"Alan",
			"Sally",
			"Lucille",
			"Charlie",
			"Arlen",
			"Sayuri",
			"Owen",
			"Brooke",
			"Clint",
			"Bonita",
			"Simon",
			"Blythe",
			"Sophie",
			"Anthony",
			"Emilia",
			"Renaud",
			"Lao",
			"Janie",
			"Colin",
			"Darcy",
			"Leonard",
			"Tyler",
			"Lilly",
			"Leah",
			"Nelson",
			"Wren",
			"Caleb",
			"Patty",
			"Alexander",
			"Jules",
			"Kirsten",
			"Gerard",
			"Madhu",
			"Josh",
			"Davey",
			"Boris",
			"Charley",
			"Pierre",
			"Jeff",
			"Alica",
			"Gwyneth",
			"Russel",
			"Mia",
			"Evan",
			"Angi",
			"Miguel",
			"Dudley",
			"Andy",
			"Winter",
			"Dirk",
			"Aurora",
			"Morimoto",
			"Tanya",
			"Murphy",
			"Shauntal",
			"Marshal",
			"Grimsley",
			"Caitlin",
			"Lina",
			"Colette",
			"Flynn",
			"Winslow",
			"Ewing",
			"Chase",
			"Elesa",
			"Burgh",
			"Skyla",
			"Cheren",
			"Roxie",
			"Clay",
			"Drayden",
			"Marlon",
			"Rival",
			"Rival",
			"Rival",
			"Terrell",
			"Isabel",
			"Rival",
			"Rival",
			"Rival",
			"Seymour",
			"Cassie",
			"Pedro",
			"Serena",
			"Albert",
			"Lin",
			"Lia & Lily",
			"Kimya",
			"Stonewall",
			"Nicky",
			"Billy Jo",
			"Kenny",
			"Molly",
			"Orville",
			"Abigail",
			"Abed",
			"Brent",
			"Bess",
			"Carleigh",
			"Carlen",
			"Daneil",
			"Danelle",
			"Elie",
			"Ellas",
			"Frederick",
			"Freira",
			"Gail",
			"Gaius",
			"Harmon",
			"Harmony",
			"Iria",
			"Ira",
			"Benga",
			"Benga",
			"Isaac",
			"Mitchell",
			"Nathan",
			"Jerome",
			"Nikola",
			"Fleming",
			"Amp�re",
			"Hubert",
			"Andrew",
			"Braven",
			"Clifford",
			"Dell",
			"Neagle",
			"Joey",
			"Silvester",
			"Stanley",
			"Zeke",
			"Rob & Sal",
			"Keenan",
			"Roland",
			"Rachel",
			"Norbert",
			"Sachiko",
			"Tara & Val",
			"Bob",
			"Jonah",
			"Bob",
			"Jonah",
			"Jonah",
			"Connor",
			"Todd",
			"Connor",
			"Todd",
			"Todd",
			"Roberto",
			"Marco",
			"Roberto",
			"Marco",
			"Marco",
			"John",
			"Lamarcus",
			"John",
			"Lamarcus",
			"Lamarcus",
			"Aspen",
			"Mari",
			"Aspen",
			"Mari",
			"Mari",
			"Ava & Aya",
			"Jenn",
			"Horton",
			"Brian",
			"Preston",
			"Stu & Art",
			"Ivan",
			"Blossom",
			"Hector",
			"Krissa",
			"Hank",
			"Jacques",
			"Marissa",
			"Alvin",
			"Mara",
			"Nicole",
			"Tihana",
			"Flo",
			"Cody",
			"Hollie",
			"Greg",
			"Chrissy",
			"Sola & Ana",
			"Pat",
			"Ian",
			"Sheldon",
			"Marian",
			"Kipp",
			"Junko",
			"Jay",
			"Glinda",
			"Sid",
			"Sinclair",
			"Reece",
			"Jones",
			"Damon",
			"Leroy",
			"Vince",
			"Laura",
			"Cleo & Rio",
			"Richard",
			"Lois",
			"Daryl",
			"Dianne",
			"Irene",
			"Serenity",
			"Forrest",
			"Galen",
			"Jeriel",
			"Azra",
			"Marcus",
			"Magnolia",
			"Thalia",
			"Crofton",
			"Myra",
			"Lowell",
			"Kyle",
			"Kaoru",
			"Harold",
			"Desiree",
			"Kenzo",
			"Summer",
			"Matt",
			"Bart",
			"Tim",
			"Arissa",
			"Mitzi",
			"Maynard",
			"Pasqual",
			"Tibor",
			"Niel",
			"Tavarius",
			"Noel",
			"Friedrich",
			"Kenneth",
			"Braid",
			"Jim & Cas",
			"Chance",
			"Reese",
			"Phillip",
			"Elron",
			"Addison",
			"Justin",
			"Chris",
			"Karl",
			"Shannon",
			"Gough",
			"Charles",
			"Charles",
			"Iris",
			"Grunt",
			"Grunt",
			"Colress",
			"Ghetsis",
			"Rood",
			"Zinzolin",
			"Shadow",
			"Dean",
			"Doyle",
			"Melina",
			"Enzio",
			"Jeanne",
			"Santino",
			"Sable",
			"Grunt",
			"Nishino",
			"Colress",
			"Grunt",
			"Nate",
			"Nate",
			"Nate",
			"Rosa",
			"Rosa",
			"Rosa",
			"Charles",
			"Charles",
			"Rival",
			"Rival",
			"Rival",
			"Cheren",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Rival",
			"Rival",
			"Rival",
			"Lucius",
			"Jerry",
			"Rhona",
			"Ron",
			"Denae",
			"Leaf",
			"Naoko",
			"Bret",
			"Malory",
			"Boone",
			"Lynette",
			"Petey",
			"Edgar",
			"Marsha",
			"Autumn",
			"Buster",
			"Eva",
			"Tyrone",
			"Bruce",
			"Mack",
			"Jimmy",
			"Mali",
			"Oriana",
			"Rayne",
			"Shane",
			"Jill",
			"Mikey",
			"Henrietta",
			"Rick",
			"Audra",
			"Sean",
			"Cheyenne",
			"Lydon",
			"Bucky",
			"Wright",
			"Jeffery",
			"Ruth",
			"Dara",
			"Stephen",
			"Vincent",
			"Chester",
			"Maya",
			"Gina",
			"Zachary",
			"Wendy",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Alberta",
			"Cynthia",
			"Jan",
			"Ilse",
			"Lana",
			"Tammy",
			"Felix",
			"Brady",
			"Clarke",
			"Caroline",
			"Zack",
			"Scott",
			"Adelaide",
			"Jeremiah",
			"Drago",
			"Tia",
			"Rich",
			"Maki",
			"Johan",
			"Mikiko",
			"Don",
			"Doug",
			"Luke",
			"Brenda",
			"Juliet",
			"Grant",
			"Benjamin",
			"Tiffany",
			"Lena",
			"Steve",
			"Tom",
			"Miki",
			"Teppei",
			"Manuel",
			"Morgann",
			"Herman",
			"Paul",
			"Cliff",
			"Dusty",
			"Chili",
			"Cress",
			"Cilan",
			"Zinzolin",
			"Shadow",
			"Shadow",
			"Grunt",
			"Clemens",
			"Warren",
			"Neil",
			"Jariel",
			"Janna",
			"Leo",
			"Brand",
			"Maxwell",
			"Katie",
			"Dua",
			"Perry",
			"Low",
			"Kiyo",
			"Kumiko",
			"Lewis",
			"Lewis",
			"Eliza",
			"Eliza",
			"Ray",
			"Cora",
			"Jared",
			"Markus",
			"Corey",
			"Chan",
			"Cairn",
			"Gus",
			"Victor",
			"Patton",
			"Ryan",
			"Hunter",
			"Wade",
			"Zach",
			"Julia",
			"Carter",
			"Chloris",
			"Iris",
			"Anna",
			"Beverly",
			"Otto",
			"Jeremy",
			"Ronald",
			"Lumi",
			"Briana",
			"Louis",
			"Vicki",
			"Corky",
			"Shaye",
			"Mary",
			"Jude",
			"Georgia",
			"Daniel",
			"Grace",
			"Micki",
			"Joyce",
			"Bryce",
			"Sarah",
			"Jaye",
			"Lurleen",
			"Wes",
			"Rae & Ula",
			"Tobias",
			"Keith",
			"Kendall",
			"Randall",
			"Annie",
			"Eileen",
			"Terrance",
			"Lumina",
			"Tully",
			"Ena",
			"Astor",
			"Mai",
			"Ingrid",
			"Tyra",
			"Ryder",
			"Eustace",
			"Arnold",
			"Parker",
			"Elaine",
			"Rocky",
			"Colress",
			"Alder",
			"Shadow",
			"Zinzolin",
			"Grunt",
			"Grunt",
			"Grunt",
			"Rival",
			"Rival",
			"Rival",
			"Nishino",
			"Morimoto",
			"Keita",
			"Billy",
			"Alia",
			"Jamie",
			"Al",
			"Beckett",
			"Shelly",
			"Cathy",
			"Logan",
			"Webster",
			"Shanta",
			"Sterling",
			"Portia",
			"Caroll",
			"Martell",
			"Elmer",
			"Chalina",
			"Pierce",
			"Chandra",
			"Hugo",
			"Mae",
			"Claude",
			"Cecile",
			"    ",
			"    ",
			"    ",
			"    ",
			"    ",
			"Rolan",
			"Abe",
			"Chaz",
			"Mariana",
			"Henry",
			"Sibyl",
			"Brad",
			"Elizandra",
			"Cassandra",
			"Robert",
			"Tanya",
			"Cheren",
			"Keston",
			"Millie",
			"Kelsey",
			"Kathrine",
			"Kentaro",
			"Lee",
			"Ralph",
			"Dwayne",
			"Melita",
			"Hillary",
			"Sinan",
			"Rosaline",
			"Keita",
			"Nicholas",
			"Ethel",
			"Jojo",
			"Jos�",
			"Samantha",
			"Ally & Amy",
			"Elliot",
			"Lydia",
			"Marie",
			"Talon",
			"Corin",
			"Abraham",
			"Eddie",
			"Elle",
			"Naoise",
			"Rich",
			"Walt",
			"Sam",
			"Tami",
			"Clara",
			"Jaden",
			"Anja",
			"Tommy",
			"Future",
			"Mariah",
			"Kit",
			"Derek",
			"Dixie",
			"Zaiem",
			"Maggie",
			"Xiao",
			"Edward",
			"Thomas",
			"Heath",
			"Jebediah",
			"Shelby",
			"Geoff",
			"Belle",
			"Rival",
			"Rival",
			"Rival",
			"Shadow",
			"Shadow",
			"Shadow",
			"Grunt",
			"Britney",
			"Gilligan",
			"Rival",
			"Rival",
			"Rival",
			"Rival",
			"Rival",
			"Rival",
			"Diana",
			"Diana",
			"Rival",
			"Rival",
			"Rival",
			"Zinzolin",
			"Grunt",
			"Bianca",
			"Bianca",
			"Bianca",
			"Cheren",
			"Cheren",
			"Cheren",
			"Rodrigo",
			"Gordon",
			"Andrey",
			"Grigor",
			"Donny",
			"Mathis",
			"Natasha",
			"Nina",
			"Eve",
			"Trish",
			"Jacqueline",
			"Layla",
			"Grunt",
			"Carol",
			"Derrick",
			"Lizzy",
			"Amy",
			"Nicolas",
			"Wesley",
			"Alize",
			"Ingo",
			"Emmet",
			"Clarence",
			"Jack",
			"Gary",
			"Anders",
			"Daya",
			"William",
			"Rita",
			"Olesia",
			"Nandor",
			"Athena",
			"Franklin",
			"Masahiro",
			"Waylon",
			"Seymour",
			"Cassie",
			"Seymour",
			"Cassie",
			"Grunt",
			"April",
			"May & Mal",
			"Ricky",
			"Jean-Paul",
			"Tina",
			"Ernest",
			"Eli",
			"Donnaven",
			"Crispin",
			"Joseph",
			"Wain",
			"Grunt",
			"Cheren",
			"Roxie",
			"Burgh",
			"Elesa",
			"Clay",
			"Skyla",
			"Drayden",
			"Marlon",
			"Shauntal",
			"Grimsley",
			"Marshal",
			"Caitlin",
			"Iris",
			"Shauntal",
			"Grimsley",
			"Marshal",
			"Caitlin",
			"Iris",
			"N",
			"N",
			"N",
			"N",
			"Ray",
			"Cora",
			"Kiyo",
			"Kumiko",
			"Rosalyn",
			"Ike",
			"Henley",
			"Helia",
			"Rival",
			"Rival",
			"Rival",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"June",
			"December",
			"Augustin",
			"January",
			"Grunt",
			"Grunt",
			"Grunt",
			"Grunt",
			"Julius",
			"Grunt",
			"Colress"
		};
    }
}