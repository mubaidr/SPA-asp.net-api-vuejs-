<template>
  <div class="full-width">
    <md-layout md-hide-medium=""></md-layout>
    <md-layout>
      <md-card class="full-width">
        <md-card-header class="indigo">
          <md-card-header-text>
            <div>
              <div class="md-title">Get started</div>
              <span>New Message</span>
            </div>
          </md-card-header-text>
          <md-spinner class="md-accent" md-indeterminate v-show="Page.isLoading"></md-spinner>
        </md-card-header>
        <md-card-content>
          <md-input-container>
            <label>Details</label>
            <md-textarea name="Description" v-model="Description"></md-textarea>
          </md-input-container>
          <md-input-container>
            <label for="Users">Send To</label>
            <md-select name="Users" multiple v-model="Users">
              <md-option v-for="user in Catalog.Users" :value="user.Id" :title="user.UserName" :disabled="user.Id == userInfo.ID">
                {{user.UserName}} 
                <span class="md-caption">{{user.Email}}</span>
                </md-option>
            </md-select>
          </md-input-container>
        </md-card-content>
        <md-card-actions>
          <md-button class="md-raised md-accent" id="btn-Submit" @click.native="formValidate" :disabled="Page.isLoading">
            <md-icon>send</md-icon>
            Send
          </md-button>
        </md-card-actions>
      </md-card>
    </md-layout>
    <md-layout md-hide-medium=""></md-layout>
  </div>
</template>
<script>
  // import _ from 'lodash'
  import { postMessage } from 'services/messages'
  import {
    getUsersList
  } from 'services/account'

  export default {
    name: 'message-create',
    data () {
      return {
        Description: 'Some text message!',
        Page: {
          isLoading: false
        },
        Users: [],
        Catalog: {
          Users: []
        }
      }
    },
    computed: { userInfo () { return this.$store.getters.getUserInfo } },
    methods: {
      sendMessage () {
        const _self = this
        var msg = _self.createMessage()
        _self.Page.isLoading = true

        postMessage(msg).then(res => {
          _self.$emit('message-sent')
        }).catch(err => {
          console.log(err.data)
        }).then(() => {
          _self.Page.isLoading = false
        })
      },
      createMessage () {
        const _self = this
        return {
          Description: _self.Description.trim(),
          Receivers: _self.Users
        }
      },
      formValidate () {
        if (this.Description.trim()) {
          this.sendMessage()
        }
      }
    },
    created () {
      const _self = this
      getUsersList().then(res => {
        _self.Catalog.Users = res.data
      }).catch(err => {
        console.log(err)
      })
    },
    mounted () {

    }
  }

</script>
<style scoped="">


</style>
