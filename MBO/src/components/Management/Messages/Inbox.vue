<template>
  <div>
    <!--<md-whiteframe md-tag="section" class="full-width">-->
    <md-toolbar class="md-transparent md-dense">
      <md-button class="md-icon-button md-accent" @click.native="$router.push({path: '/messages' })">
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
    <!--</md-whiteframe > -->
    <br />
    <md-layout>
      <md-layout md-flex-small="33" md-flex="20">
        <md-progress class="md-primary" :class="{hidden: !Catalog.loading }" md-indeterminate></md-progress>
        <ul class="contact-list">
          <li v-for="user in Catalog.Users" class="full-width text-left" @click="openChat(user)" :class="{'active' : user.Id == ActiveUser.Id}">
            <md-icon>account_circle</md-icon>
            <span>{{ user.Email }}</span>
          </li>
        </ul>
      </md-layout>
      <md-layout>
        <md-progress class="md-primary" :class="{hidden: !ActiveChat.loading }" md-indeterminate></md-progress>
        <pre>
          {{ Catalog.loading }}
        </pre>
      </md-layout>
      <md-layout></md-layout>
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
          loading: false
        },
        ActiveChat: {
          loading: false,
          data: []
        },
        ActiveUser: [],
        Catalog: {
          loading: true,
          Users: []
        }
      }
    },
    watch: {
      'ActiveUser' (a, b, c) {
        console.log(a, b, c)
      }
    },
    computed: {
      userInfo () {
        return this.$store.getters.getUserInfo
      }
    },
    methods: {
      openChat (user) {
        var _self = this
        if (user.Id !== _self.ActiveUser.Id) {
          _self.$set(_self, 'ActiveUser', user)
        }
      }
    },
    mounted () {
      const _self = this
      getUsersList().then(res => {
        _self.Catalog = {
          Users: res.data,
          loading: false
        }
      }).catch(err => {
        console.log(err)
      })
    }
  }

</script>
<style scoped>
  .contact-list {
    list-style: none;
    padding: 0;
    margin: 0;
    background-color: #fff;
    border-radius: 3px;
    width: 100%;
  }
  
  .contact-list li {
    cursor: pointer;
    margin: 0;
    padding: 15px;
    border: 1px solid #efefef;
    border-bottom: none;
    color: rgba(0, 0, 0, 0.75);
    transition: background-color 0.5s ease-out;
  }
  
  .contact-list li:last-child {
    border-bottom: 1px solid #efefef;
  }
  
  .contact-list li:not(.active):hover {
    background-color: #efefef;
    color: rgba(0, 0, 0, 0.9);
  }
  
  .contact-list li.active {
    color: #3f51b5;
    background-color: rgba(63, 81, 181, 0.05);
    border-color: rgba(63, 81, 181, 0.1);
  }

</style>
