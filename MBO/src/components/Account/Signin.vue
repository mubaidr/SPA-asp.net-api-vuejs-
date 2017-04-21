<template>
  <md-layout md-gutter>
    <md-layout md-hide-xsmall></md-layout>
    <md-layout md-hide-medium></md-layout>
    <md-layout>
      <div class="flex-vertical min-height full-width">
        <md-card class="full-width">
          <md-card-header class="indigo">
            <md-card-header-text>
              <div class="md-title">Get started</div>
              <span>Sign In</span>
            </md-card-header-text>
            <md-spinner md-indeterminate class="md-accent" v-show="state.loading"></md-spinner>
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
            <app-message :state="state"></app-message>
          </md-card-content>
          <md-card-actions>
            <router-link tag="md-button" to="/recover" class="md-accent">Forgot password?</router-link>
            <md-button id="btnSubmit" class="md-raised md-accent" @click.native="formValidate" :disabled="state.loading">Sing In</md-button>
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
    signin,
    getUserInfo
  } from 'services/account'

  export default {
    data () {
      return {
        credentials: {
          UserName: 'tester@test.com',
          Password: 'tester1234'
        },
        state: {
          loading: false,
          type: 'error',
          title: null,
          details: null
        }
      }
    },
    methods: {
      setErrorDetails (err) {
        // TODO register as global method
        // console.dir(err);
        const _self = this
        if (err) {
          _self.state.title = err.message
          if (err.response && err.response.data && err.response.data.error_description) {
            _self.state.details = err.response.data.error_description
          }
        } else {
          _self.state.title = null
        }
      },
      formValidate (event) {
        event.preventDefault()
        const _self = this

        _self.$validator.validateAll().then(success => {
          if (!success) return

          _self.setErrorDetails()

          _self.state.loading = true

          signin(this.credentials).then(res => {
            _self.$store.commit('setAuthentication', res.data)

            getUserInfo().then(res => {
              _self.$store.commit('setUserInfo', res.data)

              _self.$router.push({
                path: '/dashboard'
              })
            }).catch(err => {
              _self.setErrorDetails(err)
            }).then(() => {
              _self.state.loading = true
            })
          }).catch(err => {
            _self.setErrorDetails(err)
          }).then(() => {
            _self.state.loading = true
          })
        })
      }
    }
  }

</script>
