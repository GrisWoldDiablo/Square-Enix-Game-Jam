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

const init = () => {
  airconsole = new AirConsole({"orientation": "landscape"});
  airconsole.onMessage = onReceiveMessage;
  airconsole.onActivePlayersChange = (player_number) => {
    updatePlayersStatusText(player_number);
  }
  setUpSupportForMouseEvents();
}

const onReceiveMessage = (device_id, data) => {
  const {menu, gameResult} = data;
  
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
