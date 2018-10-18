const x = 20
const y = 20
//x and y need to be the same for now
const tileSize = 5
const wallSize = 0 //not used right now (kinda)
const ncolor = "#2196F3"
const scolorClass = "w3-red";

var map = [];
var tableG;

function createTable() {
    var dvp = document.getElementById("main");
    var tbl = document.createElement("table")
    tableG = tbl;
    //ondrop="dropHandler(event);"
    //dvp.addEventListener("drop", dropHandler);
    //dvp.addEventListener("drag", dragOverHandler);
    tbl.style.backgroundColor = ncolor;
    tbl.style.width = "100%";
    tbl.style.height = window.innerHeight + "px";
    for (var i = 0; i < y; i++) {
        var rw = document.createElement("tr");
        rw.style.backgroundColor = ncolor;
        map.push([]);
        for (var j = 0; j < x; j++) {
            var col = document.createElement("td");
            col.style.backgroundColor = ncolor;
            var dv = document.createElement("div");
            dv.setAttribute("xy", j + ":" + i);
            dv = addPlacement(dv);
            col.appendChild(dv);
            rw.appendChild(col);
            map[i].push([0, 0, 0, 0]);
        }
        tbl.appendChild(rw);
    }
    //ondrop="dropHandler(event);
    dvp.appendChild(tbl);
    document.body.appendChild(dvp);
}

function addPlacement(div) {
    div.className = "w3-display-container"

    var top = document.createElement("div")
    var left = document.createElement("div")
    var right = document.createElement("div")
    var bottom = document.createElement("div")

    top.setAttribute("tp", 0);
    left.setAttribute("tp", 1);
    right.setAttribute("tp", 2);
    bottom.setAttribute("tp", 3);

    top.className = "w3-display-topmiddle"
    bottom.className = "w3-display-bottommiddle"
    left.className = "w3-display-left"
    right.className = "w3-display-right"


    div.appendChild(left);
    div.appendChild(right);
    div.appendChild(top);
    div.appendChild(bottom);


    //var dvs = [top, bottom, left, right, div];
    //dvs.forEach((d) => { d.style.backgroundColor = ncolor; })
    div.style.backgroundColor = ncolor;

    var dvsa = [top, bottom, left, right];
    dvsa.forEach((d) => {
        //hover
        d.className += " w3-hover-red"
        //onclick
        d.onclick = () => {
            var xy = d.parentElement.getAttribute("xy")
            var lx = xy.split(":");
            var ly = lx[1];
            lx = lx[0];

            if (map[lx][ly][d.getAttribute("tp")] == 0) {
                d.className += " " + scolorClass
                map[lx][ly][d.getAttribute("tp")] = 1;
            } else {
                d.className = d.className.split(" " + scolorClass).join("");
                map[lx][ly][d.getAttribute("tp")] = 0;
            }


        };
    })

    var tb = [top, bottom];
    var lr = [left, right];
    var wx = (window.innerWidth / x);
    var hy = (window.innerHeight / y);

    var wy = hy / 4;
    var hx = wx / 4;

    div.style.width = wx + "px";
    div.style.height = hy + "px";

    tb.forEach((d) => {
        d.style.width = wx + "px"; d.style.height = wy + "px";
    })
    lr.forEach((d) => {
        d.style.height = hy + "px"; d.style.width = hx + "px";
    })

    return div;
}

function dropHandler(ev) {
    console.log('File(s) dropped');

    // Prevent default behavior (Prevent file from being opened)
    ev.preventDefault();
    var reader = new FileReader();

    if (ev.dataTransfer.items) {
        // Use DataTransferItemList interface to access the file(s)
        for (var i = 0; i < ev.dataTransfer.items.length; i++) {
            // If dropped items aren't files, reject them
            if (ev.dataTransfer.items[i].kind === 'file') {
                var file = ev.dataTransfer.items[i].getAsFile();
                console.log('... file[' + i + '].name = ' + file.name);

                reader.onload = function (e) {
                    var text = reader.result;
                    map = JSON.parse(text);
                    loadmap();
                }
                reader.readAsText(file);

            }
        }
    } else {
        // Use DataTransfer interface to access the file(s)
        for (var i = 0; i < ev.dataTransfer.files.length; i++) {
            console.log('... file[' + i + '].name = ' + ev.dataTransfer.files[i].name);

        }
        reader.onload = function (e) {
            var text = reader.result;
            map = JSON.parse(text);
            loadmap();
        }
        reader.readAsText(ev.dataTransfer.files[0]);
    }

    // Pass event to removeDragData for cleanup
    removeDragData(ev)
}

function dragOverHandler(ev) {
    console.log('File(s) in drop zone');

    // Prevent default behavior (Prevent file from being opened)
    ev.preventDefault();
}
function removeDragData(ev) {
    console.log('Removing drag data');

    if (ev.dataTransfer.items) {
        // Use DataTransferItemList interface to remove the drag data
        ev.dataTransfer.items.clear();
    } else {
        // Use DataTransfer interface to remove the drag data
        ev.dataTransfer.clearData();
    }
}

function loadmap() {
    var mapping = [2, 0, 1, 3] //[1,2,0,3];
    for (var i = 0; i < y; i++) {
        for (var j = 0; j < x; j++) {
            for (var z = 0; z < 4; z++) {
                if (map[j][i][z] === 1) {
                    //1,2,0,3
                    //console.log(tableG.children[i].children[j].children[0].children[mapping[z]])
                    //tableG.children[i].children[j].children[0].children[mapping[z]].click();
                    tableG.children[i].children[j].children[0].children[mapping[z]].className += " " + scolorClass
                } else {
                    tableG.children[i].children[j].children[0].children[mapping[z]].className = tableG.children[i].children[j].children[0].children[mapping[z]].className.split(" " + scolorClass).join("");
                }
            }
        }
    }
}

createTable();