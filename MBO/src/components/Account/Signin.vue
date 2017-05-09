<template>
  <md-layout md-gutter="">
    <md-layout md-hide-small=""></md-layout>
    <md-layout>
      <div class="flex-vertical min-height full-width">
        <md-card class="full-width">
          <md-card-header class="indigo">
            <md-card-header-text>
              <div class="md-title">Get started</div>
              <span>Sign In</span>
            </md-card-header-text>
            <md-spinner class="md-accent" md-indeterminate v-show="state.loading"></md-spinner>
          </md-card-header>
          <md-card-content>
            <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
              <label>Email</label>
              <md-input name="Email" data-vv-name="Email" data-vv-rules="required|email|min:6" type="email" v-model="credentials.UserName" v-validate></md-input>
              <span class="md-error">{{errors.first('Email')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('Password')}">
              <label>Password</label>
              <md-input name="Password" data-vv-name="Password" data-vv-rules="required|min:6" type="password" v-model="credentials.Password" v-validate></md-input>
              <span class="md-error">{{errors.first('Password')}}</span>
            </md-input-container>
            <app-message :state="state"></app-message>
          </md-card-content>
          <md-card-actions>
            <router-link class="md-accent" tag="md-button" to="/recover">Forgot password?</router-link>
            <md-button class="md-raised md-accent" id="btnSubmit" @click.native="formValidate" :disabled="state.loading">Sing In</md-button>
          </md-card-actions>
        </md-card>
      </div>
    </md-layout>
    <md-layout md-hide-small=""></md-layout>
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
    formValidate (event) {
      event.preventDefault()

      this.$validator.validateAll().then(success => {
        if (!success) return

        this.setErrorDetails()

        this.state.loading = true

        signin(this.credentials).then(res => {
          this.$store.commit('setAuthentication', res.data)

          getUserInfo().then(res => {
            this.$store.commit('setUserInfo', res.data)

            this.$router.push({
              path: '/dashboard'
            })
          }).catch(err => {
            this.setErrorDetails(err)
          }).then(() => {
            this.state.loading = true
          })
        }).catch(err => {
          this.setErrorDetails(err)
        }).then(() => {
          this.state.loading = true
        })
      })
    }
  }
}

</script>
