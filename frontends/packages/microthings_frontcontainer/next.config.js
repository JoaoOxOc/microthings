const withPlugins = require("next-compose-plugins");
const withTM = require("next-transpile-modules")(["@microthings/home"]);

module.exports = withPlugins([withTM], {
  reactStrictMode: true
}
)