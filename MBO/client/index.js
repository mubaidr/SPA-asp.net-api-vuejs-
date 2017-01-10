import Vue from 'vue';
import 'vue-material/dist/vue-material.css';
import 'assets/css/icon.css';
import 'assets/css/site.css';

import 'utilities/promise-polyfill';
import 'utilities/vue-material';
import 'moment';
import 'components/_custom';

import {
  app
} from 'app';

app.$mount('#app');

import 'utilities/veeValidate';
import 'assets/script/site';
