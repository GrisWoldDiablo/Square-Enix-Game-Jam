let airconsole;

const MOVES = Object.freeze({
  ROCK: 0,
  PAPER: 1,
  SCISSORS: 2,
});

const MENU = Object.freeze({
  LEFT: 'LEFT',
  RIGHT: 'RIGHT',
  ENTER: 'ENTER',
  EXIT: 'EXIT',
})
let MenuButton;
let GameButton;
let GameStatus;
let MRock;
let MPaper;
let MScissors;
const init = () => {
    MenuButton = document.getElementById("menubutton");
    GameButton = document.getElementById("gamebutton");
    MRock = document.getElementById("mrock");
    MPaper = document.getElementById("mpaper");
    MScissors = document.getElementById("mscissors");
  airconsole = new AirConsole({"orientation": "landscape"});
  airconsole.onMessage = onReceiveMessage;
  //airconsole.onActivePlayersChange = (player_number) => {
  //  updatePlayersStatusText(player_number);
  //}
  setUpSupportForMouseEvents();
}
let player = "";
const onReceiveMessage = (device_id, data) => {
  //const {menu, gameResult} = data;
    switch (data) {
        case "PLAYER1":
            player = "You are Player 1";
            SetPlayerText();
            break;
        case "PLAYER2":
            player = "You are Player 2";
            SetPlayerText();
            break;
        case "INMENU":
            SetPlayerText();
            GameStatus = "InMenu";
            ToggleGameButton(false);
            ToggleMenuButton(true);
            break;
        case "INGAME":
            SetPlayerText();
            GameStatus = "InGame";
            ToggleGameButton(true);
            ToggleMenuButton(false);
            break;
        case "MISSINGPLAYERS":
            UpdateStatusText("Waiting for an other player...");
            ToggleGameButton(false);
            ToggleMenuButton(false);
            break;
        case "ALLPLAYERS":
            SetPlayerText();
            if (GameStatus === "InMenu") {
                ToggleMenuButton(true);
            }
            else {
                ToggleGameButton(true);
            }
            break;
        case "GAMEFULL":
            UpdateStatusText("The game is full please disconnect.");
            ToggleGameButton(false);
            ToggleMenuButton(false);
            break;
        case "ROUNDREADY":
            ResetButtons();
            break;
        case "WINNER":
            UpdateStatusText("!!! WINNER !!!");
            ToggleGameButton(false);
            ToggleMenuButton(false);
            break;
        case "LOSER":
            UpdateStatusText("... LOSER ...");
            ToggleGameButton(false);
            ToggleMenuButton(false);
            break;
    }
    //document.getElementById('player-status').innerHTML = data;
}

const updatePlayersStatusText = (player_number) => {
  const playerStatusDiv = document.getElementById('player-status');
  if (airconsole.getActivePlayerDeviceIds().length == 0) {
    playerStatusDiv.innerHTML = "Waiting for more players.";
  } else if (player_number == undefined) {
    playerStatusDiv.innerHTML = "This is a 2 player game";
  } else if (player_number == 0) {
    playerStatusDiv.innerHTML = "You are the first player";
  } else if (player_number == 1) {
    playerStatusDiv.innerHTML = "You are the second player";
  };
}

const sendMove = (move) => {
    switch (move) {
        case 0:
            MPaper.classList.add("hidden");
            MScissors.classList.add("hidden");
            break;
        case 1:
            MScissors.classList.add("hidden");
            MRock.classList.add("hidden");
            break;
        case 2:
            MPaper.classList.add("hidden");
            MRock.classList.add("hidden");
            break;
        default:
    }
  airconsole.message(AirConsole.SCREEN, { move });
}

const sendMenuOption = (menu) => {
  airconsole.message(AirConsole.SCREEN, { menu });
}

const setUpSupportForMouseEvents = () => {
  /*
    * Here we are adding support for mouse events manually.
    * --> WE STRONGLY ENCOURAGE YOU TO USE THE AIRCONSOLE CONTROLS LIBRARY
    * WHICH IS EVEN BETTER (BUT WE DONT WANT TO BLOAT THE CODE HERE).
    * https://github.com/AirConsole/airconsole-controls/
    * 
    * DO NOT USE ONCLICK HANDLERS.
    * THEY ADD A 200MS DELAY!
    */
    if (!("ontouchstart" in document.createElement("div"))) {
    var elements = document.getElementsByTagName("*");
    for (var i = 0; i < elements.length; ++i) {
      var element = elements[i];
      var ontouchstart = element.getAttribute("ontouchstart");
      if (ontouchstart) {
        element.setAttribute("onmousedown", ontouchstart);
      }
      var ontouchend = element.getAttribute("ontouchend");
      if (ontouchend) {
        element.setAttribute("onmouseup", ontouchend);
      }
    }
  }
}

function ToggleGameButton(show) {
    if (GameButton === null) {
        GameButton = document.getElementById("gamebutton");
    }
    if (show === false) {
        GameButton.classList.add("hidden");
    }
    else {
        GameButton.classList.remove("hidden");
    }
}

function ToggleMenuButton(show) {
    if (MenuButton === null) {
        MenuButton = document.getElementById("menubutton");
    }
    if (show === false) {
        MenuButton.classList.add("hidden");
    }
    else {
        MenuButton.classList.remove("hidden");
    }
}

function SetPlayerText() {
    document.getElementById('player-status').innerHTML = player;
}

function UpdateStatusText(status) {
    document.getElementById('player-status').innerHTML = status;
}

function ResetButtons() {
    MRock.classList.remove("hidden");
    MPaper.classList.remove("hidden");
    MScissors.classList.remove("hidden");
}
