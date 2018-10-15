var verts = -1; //-1 offset built in
function createModel(file) {
    verts = -1;
    var geometry = createFloorGeometry() //new THREE.BoxGeometry(1, 1, 1);
    var material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    var material2 = new THREE.MeshBasicMaterial({ color: 0xffff00 });
    

    geometry = createWalls(geometry);

    var cube = new THREE.Mesh(geometry, [material, material2]);

    var exporter = new THREE.ColladaExporter();

    exporter.parse(cube, function (result) {

        //saveString(result.data, 'scene.dae');
        download(file, result.data);
    });
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
    var gv = [];
    if (side == 0 || side == 3) {
        //top/bottom
        var vertsz = [
            [posx, 0, posz],
            [posx + tileSize, 0, posz],
            [posx, 5, posz],
            [posx + tileSize, tileSize, posz]
        ];
        if (side == 3) {
            vertsz.forEach((v)=>{v[2]+= tileSize})
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
            vertsz.forEach((v)=>{v[0]+= tileSize})
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

    verts += 4;

    geometry.faces.push(face);
    geometry.faces.push(face2);

    geometry.computeBoundingBox();

    return geometry;
}

function arrayToV3(arr) {
    console.log(arr);
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
    verts += 4;

    var normal = new THREE.Vector3(0, 1, 0); //optional
    var color = new THREE.Color(0xffaa00); //optional
    var materialIndex = 0; //optional
    var face = new THREE.Face3(0, 1, 2, normal, color, materialIndex);
    var face2 = new THREE.Face3(1, 2, 3, normal, color, materialIndex);

    geometry.faces.push(face);
    geometry.faces.push(face2);

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