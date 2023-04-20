mergeInto(LibraryManager.library, {

    LoadPlayerData: function () {
    	player.getData().then(_date => {
        	const myJSON = JSON.stringify(_date);
    	});
      return myJSON;
    },

    SavePlayerData: function (data) {
    	var dateString = UTF8ToString(data);
    	var myobj = JSON.parse(dateString);
    	player.setData(myobj);
    },

    ShowRewardAds: function () {
    },

    ShowFullscreenAds: function () {
    },
  
  });