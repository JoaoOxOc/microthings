const withPlugins = require("next-compose-plugins");
const withTM = require("next-transpile-modules")(["@microthings/home", "@microthings/notifications_bar"]);

module.exports = withPlugins([withTM], {
  reactStrictMode: true
}
)