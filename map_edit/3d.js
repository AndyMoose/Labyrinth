var verts = -1; //-1 offset built in
const scale = 5;
var scene
var movement = [];
function createModel() {
    verts = -1;
    file = "file.dae"
    clearMovement();
    scene = new THREE.Scene();
    var geometryFloor = createFloorGeometry() //new THREE.BoxGeometry(1, 1, 1);
    var geometry = new THREE.Geometry();
    // /var texture = new THREE.TextureLoader().load('textures/floor_small.jpg');

    var material = new THREE.MeshBasicMaterial({ color: 0xff00ff });
    var material2 = new THREE.MeshBasicMaterial({ color: 0xffff00 });



    geometry = createWalls(geometry);
    var floor = new THREE.Mesh(geometryFloor, [material, material2]);
    var cube = new THREE.Mesh(geometry, [material, material2]);

    scene.add(floor);
    scene.add(cube);

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
            for (var z = 0; z < scale; z++) {
                movement[sx + z][sy + scale - 1] = 1;
            }

        } else {
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
            for (var z = 0; z < scale; z++) {
                movement[sx + scale - 1][sy + z] = 1;
            }

        } else {
            for (var z = 0; z < scale; z++) {
                movement[sx + scale - 1][sy + z] = 1;
            }
        }
        gv = vertsz;
    }

    gv.forEach((v) => { geometry.vertices.push(arrayToV3(v)) });

    var normal = new THREE.Vector3(0, 1, 0); //optional
    var color = new THREE.Color(0xffaa00); //optional
    var materialIndex = 1; //optional
    //console.log(geometry.vertices);
    //console.log(verts);
    //console.log(geometry.vertices.length);
    var face = new THREE.Face3(verts + 1, verts + 2, verts + 3, normal, color, materialIndex);
    var face2 = new THREE.Face3(verts + 2, verts + 3, verts + 4, normal, color, materialIndex);
    var face3 = new THREE.Face3(verts + 3, verts + 2, verts + 1, normal, color, materialIndex);
    var face4 = new THREE.Face3(verts + 4, verts + 3, verts + 2, normal, color, materialIndex);

    verts += 4;

    geometry.faces.push(face);
    geometry.faces.push(face2);
    geometry.faces.push(face3);
    geometry.faces.push(face4);

    geometry.computeBoundingBox();

    return geometry;
}

function arrayToV3(arr) {
    //console.log(arr);
    return new THREE.Vector3(arr[0], arr[1], arr[2]);
}

function createFloorGeometry() {
    var geometry = new THREE.Geometry();

    geometry.vertices.push(
        //center at 0,0 rather than edge prob need to change it
        // (or change createwall to place in -x/z)
        new THREE.Vector3(-0 * x * tileSize, 0, -0 * y * tileSize), //x, y, z
        new THREE.Vector3(-0 * x * tileSize, 0, 1 * y * tileSize),
        new THREE.Vector3(1 * x * tileSize, 0, -0 * y * tileSize),
        new THREE.Vector3(1 * x * tileSize, 0, 1 * y * tileSize)
    );
    //verts += 4;

    var normal = new THREE.Vector3(0, 1, 0); //optional
    var color = new THREE.Color(0xffaa00); //optional
    var materialIndex = 0; //optional
    var face = new THREE.Face3(0, 1, 2, normal, color, materialIndex);
    var face2 = new THREE.Face3(1, 2, 3, normal, color, materialIndex);

    var face3 = new THREE.Face3(2, 1, 0, normal, color, materialIndex);
    var face4 = new THREE.Face3(3, 2, 1, normal, color, materialIndex);

    geometry.faces.push(face);
    geometry.faces.push(face2);
    geometry.faces.push(face3);
    geometry.faces.push(face4);

    geometry.computeBoundingBox();
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