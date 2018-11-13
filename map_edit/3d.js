var verts = -1; //-1 offset built in
const scale = 5;
var scene
var movement = [];
var walls = [];
function createModel() {
    verts = -1;
    file = "file.dae"
    clearMovement();
    scene = new THREE.Scene();
    var geometryFloor = createFloorGeometry() //new THREE.BoxGeometry(1, 1, 1);
    var geometry = new THREE.Geometry();
    // /var texture = new THREE.TextureLoader().load('textures/floor_small.jpg');

    var material = new THREE.MeshBasicMaterial({ color: 0xFFFFFF });
    var material2 = new THREE.MeshBasicMaterial({ color: 0xFFFFFF });


    walls = [];
    geometry = createWalls(geometry);
    var floor = new THREE.Mesh(geometryFloor, [material, material2]);
    //var cube = new THREE.Mesh(geometry, [material, material2]);
    walls.forEach((wall) => {
        wall.faces.forEach((face)=>{
            face.materialIndex = 2;
        });
        var msh = new THREE.Mesh(wall, [material, material2])
        msh.position.set(wall.position.x, wall.position.y + 3, wall.position.z)
        scene.add(msh);
    })
    //floor.position.set(wallSize * x / 2, 0, wallSize * y / 2);
    scene.add(floor);
    console.log({ scene })
    //scene.add(cube);

    var exporter = new THREE.ColladaExporter();

    exporter.parse(scene, function (result) {





        //saveString(result.data, 'scene.dae');
        //download(file, result.data);
        //download(file + ".txt", JSON.stringify(map));
        //download(file + "_movment.txt", JSON.stringify(movement));

        var zip = new JSZip();
        zip.file(file, result.data);
        zip.file(file + ".txt", JSON.stringify(map));
        zip.file(file + "_movment.txt", JSON.stringify(map));
        zip.file(file + ".obj", exportToObj(scene));
        //var tex = zip.folder("textures");
        //tex.file("floor_small.jpg", getBase64Image(imgz), { base64: true });

        zip.generateAsync({ type: "blob" })
            .then(function (content) {
                // see FileSaver.js
                //download("map.zip", content);
                saveAs(content, "map.zip");
            });

    });
}

function clearMovement() {

    movement = [];
    for (var i = 0; i < y * scale; i++) {
        movement.push([]);
        for (var j = 0; j < x * scale; j++) {
            //console.log(JSON.stringify(movement));
            movement[i].push(0);
        }
    }
}

function createWalls(geometry) {
    for (var i = 0; i < y; i++) {
        for (var j = 0; j < x; j++) {
            for (var z = 0; z < 4; z++) {
                if (map[j][i][z] == 1) {
                    //console.log(j+":"+i+":"+z);
                    geometry = createWall(j, i, z, geometry);
                }
            }
        }
    }

    return geometry;
}
function createWall(x, y, side, geometry) {
    var posx = x * tileSize;
    var posz = y * tileSize; //0 top, 3 bottom, 1 left, 2 right

    //lights
    //var light = new THREE.PointLight(0xff0000, 1, 100);
    //light.position.set(posx, 5, posz);
    //scene.add(light);

    var sx = scale * x;
    var sy = scale * y;

    var gv = [];
    if (side == 0 || side == 3) {
        //top/bottom
        var vertsz = [
            [posx, 0, posz],
            [posx + tileSize, 0, posz],
            [posx, 5, posz],
            [posx + tileSize, tileSize, posz]
        ];
        //scale x/y

        if (side == 3) {
            vertsz.forEach((v) => { v[2] += tileSize })

            geometry = new THREE.BoxGeometry(tileSize, tileSize, wallSize);
            geometry.position = new THREE.Vector3(posx + tileSize / 2, 0, posz + tileSize);

            for (var z = 0; z < scale; z++) {
                movement[sx + z][sy + scale - 1] = 1;
            }

        } else {

            geometry = new THREE.BoxGeometry(tileSize, tileSize, wallSize);
            geometry.position = new THREE.Vector3(posx + tileSize / 2, 0, posz);

            for (var z = 0; z < scale; z++) {
                movement[sx + z][sy] = 1;
            }
        }
        gv = vertsz;
    } else {
        //left/right
        var vertsz = [
            [posx, 0, posz],
            [posx, 0, posz + tileSize],
            [posx, 5, posz],
            [posx, 5, posz + tileSize]
        ];
        if (side == 2) {
            vertsz.forEach((v) => { v[0] += tileSize })

            geometry = new THREE.BoxGeometry(wallSize, tileSize, tileSize);
            geometry.position = new THREE.Vector3(posx + tileSize , 0, posz + tileSize / 2);

            for (var z = 0; z < scale; z++) {
                movement[sx + scale - 1][sy + z] = 1;
            }

        } else {

            geometry = new THREE.BoxGeometry(wallSize, tileSize, tileSize);
            geometry.position = new THREE.Vector3(posx, 0, posz + tileSize / 2);

            for (var z = 0; z < scale; z++) {
                movement[sx + scale - 1][sy + z] = 1;
            }
        }
        gv = vertsz;
    }

    var materialIndex = 1; //optional

    walls.push(geometry);
    return geometry;
}

function arrayToV3(arr) {
    //console.log(arr);
    return new THREE.Vector3(arr[0], arr[1], arr[2]);
}

function createFloorGeometry() {

    //center at 0,0 rather than edge prob need to change it
    // (or change createwall to place in -x/z)
    new THREE.Vector3(-0 * x * tileSize, 0, -0 * y * tileSize), //x, y, z
        new THREE.Vector3(-0 * x * tileSize, 0, 1 * y * tileSize),
        new THREE.Vector3(1 * x * tileSize, 0, -0 * y * tileSize),
        new THREE.Vector3(1 * x * tileSize, 0, 1 * y * tileSize)

    var geometry = new THREE.BoxGeometry(x * tileSize, 0, y * tileSize, x, x, x);
    geometry.position = new THREE.Vector3(0, 0, 0);

    return geometry;
}

function download(filename, text) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
}

/*function getBase64Image(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
}*/

function exportToObj(obj) {

    var exporter = new THREE.OBJExporter();
    var result = exporter.parse(obj);
    return result;

}

function randomMap() {
    for (var i = 0; i < y; i++) {
        for (var j = 0; j < x; j++) {
            for (var z = 0; z < 4; z++) {
                if (Math.random() > .5) {
                    map[j][i][z] = 1;
                }
            }

        }
    }
    loadmap();
}