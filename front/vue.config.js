module.exports = {
  outputDir: '../back/wwwroot',
  devServer: {
      proxy: 'https://localhost:5001'
  },
  transpileDependencies: [
    'vuetify'
  ]
}
