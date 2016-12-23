<template>
  <div>
    <md-layout md-gutter>
      <md-layout md-hide-small></md-layout>
      <md-layout>
        <md-card class="full-width">
          <md-card-media>
            <md-ink-ripple></md-ink-ripple>
            <img src="../../assets/backgrounds/31.jpg" alt="sun" class="card-media-animation">
          </md-card-media>
          <md-card-header>
            <md-card-header-text>
              <div class="md-title">Sign In to your account</div>
              <div class="md-subhead">Please provide required information.</div>
            </md-card-header-text>
            <md-spinner md-indeterminate class="md-accent" v-show="progress.loading"></md-spinner>
            <!--<md-menu md-size="4" md-direction="bottom left">
              <md-button class="md-icon-button" md-menu-trigger>
                <md-icon>more_vert</md-icon>
              </md-button>
              <md-menu-content>
                <md-menu-item>
                  <router-link to="/signin">Sign In</router-link>
                </md-menu-item>
                <md-menu-item>
                  <router-link to="/contact">Contact us</router-link>
                </md-menu-item>
              </md-menu-content>
            </md-menu>-->
          </md-card-header>
          <md-card-content>
            <md-input-container>
              <label>Username</label>
              <md-input v-model="credentials.email" type="email" name="email" v-model.trim="credentials.email" @input="$v.credentials.email.$touch()"></md-input>
            </md-input-container>
            <md-input-container md-has-password>
              <label>Password</label>
              <md-input v-model="credentials.password" type="password" name="password" v-model.trim="credentials.password" @input="$v.credentials.password.$touch()"></md-input>
            </md-input-container>
          </md-card-content>
          <md-card-actions>
            <router-link tag="md-button" to="/recover" class="md-accent">Forgot password?</router-link>
            <md-button class="md-raised md-accent" @click="sigin">Sign In</md-button>
          </md-card-actions>
          <md-card-content>
            <span class="md-error"></span>
            <div v-show="progress.statusMessage" class="md-error">
              {{progress.statusMessage}}
            </div>
          </md-card-content>
        </md-card>
      </md-layout>
      <md-layout md-hide-small></md-layout>
    </md-layout>
  </div>
</template>
<script>
  import {
    required,
    sameAs,
    minLength,
    alphaNum,
    alpha
  } from 'vuelidate/lib/validators'
  //import email from 'utilities/validators/email'
  import axios from 'axios'
  // import 'utilities/string'
  // import {
  //   Summary
  // } from 'utilities/validationSummary'
  import {
    signin
  } from 'services/account'

  export default {
    data: function () {
      return {
        credentials: {
          email: '',
          password: ''
        },
        progress: {
          loading: false,
          statusMessage: ''
        }
      }
    },
    validations: {
      credentials: {
        email: {
          required,
          alpha,
          minLength: minLength(5)
        },
        password: {
          required,
          alphaNum,
          minLength: minLength(5)
        }
      }
    },
    computed: {
      //validationSummary: Summary
    },
    methods: {
      sigin: function (event) {
        event.preventDefault();
        var _self = this;
        _self.$v.$touch();

        if (_self.$v.$invalid) {
          return false;
        } else {

          signin({
            Email: this.credentials.email,
            Password: this.credentials.password
          }).then(function (res) {
            console.log(res);
          }).catch(function (err) {
            console.log(err);
          });

        }
      }
    },
    ready: function () {
      console.log('ready');
    }
  }

</script>
