<template>
  <div class="flex-vertical min-height full-width">
    <md-layout md-hide-small></md-layout>
    <md-layout>
      <md-card class="full-width">
        <md-card-header class="indigo">
          <md-card-header-text>
            <div>
              <div class="md-title">Get started</div>
              <span>New Message</span>
            </div>
          </md-card-header-text>
          <md-spinner md-indeterminate class="md-accent" v-show="Page.isLoading"></md-spinner>
        </md-card-header>
        <md-card-content>
          <md-input-container>
            <label>Details</label>
            <md-textarea v-model="Description" maxlength="200" name="Description"></md-textarea>
          </md-input-container>
          <md-input-container>
            <label for="Users">Send To</label>
            <md-select name="Users" multiple v-model="Users">
              <md-option v-for="user in Catalog.Users" :value="user.Id" :title="user.UserName">{{user.UserName}}</md-option>
            </md-select>
          </md-input-container>
        </md-card-content>
        <md-card-actions>
          <md-button id="btn-Submit" class="md-raised md-accent" @click.native="formValidate" :disabled="Page.isLoading">
            <md-icon>send</md-icon>
            Send
          </md-button>
        </md-card-actions>
      </md-card>
    </md-layout>
    <md-layout md-hide-small></md-layout>
  </div>
</template>
<script>
  import {
    getUsersList
  } from 'services/account'

  export default {
    name: 'message-create',
    data () {
      return {
        Description: '',
        Page: {
          isLoading: false
        },
        Users: [],
        Catalog: {
          Users: []
        }
      }
    },
    methods: {
      formValidate () {

      }
    },
    mounted () {
      const _self = this

      getUsersList().then(res => {
        _self.$set(_self.Catalog, 'Users', res.data)
      }).catch(err => {
        console.log(err)
      })
    }
  }

</script>
<style scoped>


</style>
