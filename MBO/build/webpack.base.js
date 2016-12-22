'use strict'
const path = require('path')
const webpack = require('webpack')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const config = require('./config')
const _ = require('./utils')

module.exports = {
  devtool: 'source-map',
  entry: {
    client: './client/index.js',
    vendor: [
      'axios',
      'pinkie-promise',
      'vue-material',
      'vue-router',
      'vuex',
      'vuex-router-sync'
    ]
  },
  output: {
    path: _.outputPath,
    filename: '[name].js',
    publicPath: '/'
  },
  resolve: {
    extensions: ['', '.js', '.vue', '.css', '.json'],
    alias: {
      root: path.join(__dirname, '../client'),
      components: path.join(__dirname, '../client/components')
    },
    modules: ['client', 'node_modules'],
  },
  module: {
    loaders: [{
      test: /\.vue$/,
      loaders: ['vue']
    }, {
      test: /\.js$/,
      loaders: ['babel'],
      exclude: [/node_modules/]
    }, {
      test: /\.es6$/,
      loaders: ['babel']
    }, {
      test: /\.(ico|jpg|jpeg|png|gif|eot|otf|webp|svg|ttf|woff|woff2)(\?.*)?$/,
      loader: 'file',
      query: {
        name: 'assets/img/[name].[hash:8].[ext]'
      }
    }]
  },
  babel: config.babel,
  postcss: config.postcss,
  vue: {
    loaders: {},
    postcss: config.postcss
  },
  plugins: [
    new HtmlWebpackPlugin({
      title: config.title,
      template: __dirname + '/index.html',
      filename: _.outputIndexPath
    }),
    new webpack.optimize.CommonsChunkPlugin({
      names: ['manifest', 'client', 'vendor']
    })
  ],
  target: _.target
}
