let airconsole;

const MOVES = Object.freeze({
  ROCK: 0,
  PAPER: 1,
  SCISSORS: 2,
})

function init() {
  airconsole = new AirConsole({"orientation": "landscape"});
  setUpSupportForMouseEvents();
}

/**
 * Send a message to screen with content: {'move': 0|1|2}
 */
function sendMove(move) {
  alert('sending move: ', move)
  airconsole.message(AirConsole.SCREEN, {move})
}

function setUpSupportForMouseEvents() {
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

function choice(value) {
  airconsole.message(AirConsole.SCREEN, {choice: value})
}


