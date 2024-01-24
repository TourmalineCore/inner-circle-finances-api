function() {
  var env = karate.env;
  var baseUrl = 'http://docker:5000/api';

  if (env === 'dev') {
    baseUrl = 'https://innercircle.tourmalinecore.com/api';
  }
  
  return {
    baseUrl: baseUrl
  };
}