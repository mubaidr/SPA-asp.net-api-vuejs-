<template>
  <md-layout md-gutter>
    <md-layout md-hide-xsmall></md-layout>
    <md-layout md-hide-medium></md-layout>
    <md-layout>
      <div class="flex-vertical full-width">
        <md-card class="full-width">
          <md-card-header class="indigo">
            <md-card-header-text>
              <div class="md-title">Get started</div>
              <span>Sign In</span>
            </md-card-header-text>
            <md-spinner md-indeterminate class="md-accent" v-show="isLoading"></md-spinner>
          </md-card-header>
          <md-card-content>
            <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
              <label>Email</label>
              <md-input v-model="credentials.UserName" type="email" name="Email" v-validate data-vv-name="Email" data-vv-rules="required|email|min:6"></md-input>
              <span class="md-error">{{errors.first('Email')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('Password')}">
              <label>Password</label>
              <md-input v-model="credentials.Password" type="password" name="Password" v-validate data-vv-name="Password" data-vv-rules="required|min:6"></md-input>
              <span class="md-error">{{errors.first('Password')}}</span>
            </md-input-container>
            <app-message></app-message>
          </md-card-content>
          <md-card-actions>
            <router-link tag="md-button" to="/recover" class="md-accent">Forgot password?</router-link>
            <md-button id="btnSubmit" class="md-raised md-accent" @click="formValidate" :disabled="isLoading">Sing In</md-button>
          </md-card-actions>
        </md-card>
      </div>
    </md-layout>
    <md-layout md-hide-xsmall></md-layout>
    <md-layout md-hide-medium></md-layout>
  </md-layout>
</template>
<script>
  import {
    signin
  } from 'services/account'
  import appMessage from 'components/_custom/app-message.vue'

  export default {
    components: {
      'app-message': appMessage
    },
    data: function () {
      return {
        credentials: {
          UserName: 'tester@test.com',
          Password: 'tester1234'
        }
      }
    },
    computed: {
      isLoading: function () {
        return this.$store.getters.isLoading;
      },
    },
    methods: {
      formValidate: function (event) {
        event.preventDefault();
        var _self = this;

        _self.$validator.validateAll().then(success => {
          if (!success) return;

          _self.$store.commit('setState', {
            loading: true,
            alert: false
          });

          signin(this.credentials).then(function (res) {

            _self.$store.commit('setAuthentication', res.data);

            _self.$router.push({
              name: 'dashboard'
            });

          }).catch(function (err) {

            _self.$store.commit('isNotLoading');
            _self.$store.commit('setState', {
              err: err
            });

          });

        });
      }
    }
  }

</script>
