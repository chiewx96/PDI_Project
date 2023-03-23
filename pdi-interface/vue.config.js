const { defineConfig } = require("@vue/cli-service");
const fs = require("fs");
module.exports = defineConfig({
  lintOnSave: true,
  devServer: {
    port: 7100, // CHANGE YOUR PORT HERE!
    https: {
      key: fs.readFileSync("./key.pem"),
      cert: fs.readFileSync("./cert.pem"),
    },
  },
  chainWebpack: (config) => {
    config.module
      .rule("vue")
      .use("vue-loader")
      .tap((options) => ({
        ...options,
        compilerOptions: {
          // treat any tag that starts with ion- as custom elements
          isCustomElement: (tag) => tag.startsWith("ion-"),
        },
      }));
  },
});
