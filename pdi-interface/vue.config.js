const { defineConfig } = require("@vue/cli-service");
const fs = require("fs");
module.exports = defineConfig({
  devServer: {
    port: 7100, // CHANGE YOUR PORT HERE!
    https: {
      key: fs.readFileSync("./key.pem"),
      cert: fs.readFileSync("./cert.pem"),
    },
  },
});
