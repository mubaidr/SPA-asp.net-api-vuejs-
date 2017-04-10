<template>
  <div>
    <!--<md-whiteframe md-tag="section" class="full-width">-->
      <md-toolbar class="md-transparent md-dense">
        <md-button class="md-icon-button md-accent" @click.native="$router.push({ path: '/messages' })">
          <md-tooltip md-direction="top">Inbox</md-tooltip>
          <md-icon>arrow_back</md-icon>
        </md-button>
        <span>Send Message</span>
        <span style="flex: 1"></span>
        <md-button class="md-accent" @click.native="$router.push({path: '/messages/trash'})">
          <md-tooltip md-direction="top">Archived</md-tooltip>
          <md-icon>delete</md-icon>
          Trash
        </md-button>
        <md-button class="md-accent" @click.native="$router.push({path: '/tasks'})">
          <md-tooltip md-direction="top">Tasks</md-tooltip>
          <md-icon>assignment</md-icon>
          Tasks
        </md-button>
        <md-button class="md-accent" @click.native="$router.push({path: '/dashboard'})">
          <md-tooltip md-direction="top">Dashboard</md-tooltip>
          <md-icon>dashboard</md-icon>
          Dashboard
        </md-button>
      </md-toolbar>
    <!--</md-whiteframe>-->
    <br/>
    <md-layout>
Another thing
    </md-layout>
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
        Page: {
          isLoading: false
        },
        Users: [],
        Catalog: {
          Users: []
        }
      }
    },
    methods: {},
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
<style></style>
