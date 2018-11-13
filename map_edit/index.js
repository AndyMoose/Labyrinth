var http = require('http');
var url = require('url');
var fs = require("fs");
var path = require('path');

var server = http.createServer(function (req, res) {
    //console.log(req.url);
    if (req.url === "/") {
        res.writeHead(200, { 'Content-Type': 'text/html' });
        fs.readFile("index.html", (err, data)=>{
            res.write(data.toString());
            res.end();
            return "";
        });
        
    } else if (req.url.endsWith(".css") || req.url.endsWith(".js") || req.url.endsWith(".png") || req.url.endsWith(".webm") || req.url.endsWith(".jpg") || req.url.endsWith(".html")) {
        var filePath = '.' + req.url;
        if (filePath == './')
            filePath = './home.html';

        var extname = path.extname(filePath);
        var contentType = 'text/html';
        switch (extname) {
            case '.js':
                contentType = 'text/javascript';
                break;
            case '.css':
                contentType = 'text/css';
                break;
            case '.json':
                contentType = 'application/json';
                break;
            case '.png':
                contentType = 'image/png';
                break;
            case '.jpg':
                contentType = 'image/jpg';
                break;
            case '.wav':
                contentType = 'audio/wav';
                break;
            case '.webm':
                contentType = 'video/webm';
                break;
        }

        fs.readFile(filePath, function (error, content) {
            if (error) {
                if (error.code == 'ENOENT') {
                    fs.readFile('./404.html', function (error, content) {
                        res.writeHead(200, { 'Content-Type': contentType });
                        res.end(content, 'utf-8');
                    });
                }
                else {
                    res.writeHead(500);
                    res.end('Sorry, check with the site admin for error: ' + error.code + ' ..\n');
                    res.end();
                }
            }
            else {
                res.writeHead(200, { 'Content-Type': contentType });
                res.end(content, 'utf-8');
            }
        });
    }
});
server.listen(80);