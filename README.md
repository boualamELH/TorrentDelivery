# TorrentDelivery [![Build Status on Travis:](https://travis-ci.org/INPT-org/TorrentDelivery.svg?branch=master)](https://travis-ci.org/INPT-org/TorrentDelivery)

You have favourite tv shows that you don't wanna miss the moment they are available ? TorrentDelivery is made for you :tv: !

### Roadmap

 - Add Contribution rules to the repo.
 - Add SQLite layer to store Download history and data.
 - Add Logging mechanism (using `log4net`)
 - Add Windows tasks scheduler to check for new episodes every X days
 - Create a file oriented settings system. (xml config file that goes with the app)
 - Create a p2p client (uTorrent) API to handle torrents download out of the app. (i.e. this app only handles get the `.torrents` files and leaves download to uTorrent).
 - Add a wrapper over a TV shows API to get episodes release dates.
 - Use Modern UI Forms
 
#### Libraries

 - log4net for logging
 - HtmlAgilityPack for parsing HTML pages to extract download links
 - ...
  