function createModel(file) {
    var geometry = createFloorGeometry() //new THREE.BoxGeometry(1, 1, 1);
    var material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    var cube = new THREE.Mesh(geometry, material);

    for (var i = 0; i < y; i++) {
        for (var j = 0; j < x; j++) {
            
        }
    }

    var exporter = new THREE.ColladaExporter();

    exporter.parse(cube, function (result) {

        //saveString(result.data, 'scene.dae');
        download(file, result.data);
    });
}

function createFloorGeometry() {
    var geometry = new THREE.Geometry();

    geometry.vertices.push(
        new THREE.Vector3(-1 * x * tileSize, 0, -1 * y * tileSize), //x, y, z
        new THREE.Vector3(-1 * x * tileSize, 0, 1 * y * tileSize),
        new THREE.Vector3(1 * x * tileSize, 0, -1 * y * tileSize),
        new THREE.Vector3(1 * x * tileSize, 0, 1 * y * tileSize)
    );

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