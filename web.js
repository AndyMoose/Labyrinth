const x = 20
const y = 20
const tileSize = 5
const ncolor = "#2196F3"
const scolorClass = "w3-red";

var map = [];

function createTable() {
    var tbl = document.createElement("table")
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
    document.body.appendChild(tbl);
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


    var dvs = [top, bottom, left, right, div];
    dvs.forEach((d) => { d.style.backgroundColor = ncolor; })

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



createTable();