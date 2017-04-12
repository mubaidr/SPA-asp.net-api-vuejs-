<template>
  <div>
    <!--<md-whiteframe md-tag="section" class="full-width">-->
    <md-toolbar class="md-transparent md-dense">
      <md-button class="md-icon-button md-accent" @click.native="$router.push({path: '/dashboard' })">
        <md-tooltip md-direction="top">Dashboard</md-tooltip>
        <md-icon>arrow_back</md-icon>
      </md-button>
      <span>Messages</span>
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
    <md-layout md-gutter="24">
      <md-layout md-flex-small="33" md-flex="20">
        <md-whiteframe md-tag="section" class="full-width">
          <md-progress class="md-primary" :class="{hide: !Catalog.loading }" md-indeterminate></md-progress>
          <ul class="contact-list">
            <li v-for="user in Catalog.Users" class="full-width text-left" @click="openChat(user)" :class="{'active' : ActiveUser && user.Id == ActiveUser.Id}">
              <md-icon>account_circle</md-icon>
              <span>{{ user.Email }}</span>
            </li>
          </ul>
        </md-whiteframe>
      </md-layout>
      <md-layout>
        <md-whiteframe md-tag="section" class="full-width">
          <md-progress class="md-primary" :class="{hide: !ActiveChat.loading }" md-indeterminate></md-progress>
          <div class="padded-container">
            <div class="scroll">
              <div v-if="ActiveUser != null">
                <ul class="chat-list" v-if="ActiveChat.data.length">
                  <li v-for="chat in ActiveChat.data">
                    <pre>{{chat}}</pre>
                  </li>
                </ul>
                <div v-else>
                  <p class="text-center">
                    <span class="md-caption">Say hello!</span>
                    <p class="text-center">This is beigning of your conversation with {{ActiveUser.Email}}</p>
                  </p>
                </div>
              </div>
              <div v-else>
                <p>
                  <ul>
                    <li>Find the name of the person you'd like to send message. </li>
                    <li>Click the person's name/email to open a message history. </li>
                    <li>Enter your message in the text field below, and press Enter/Send.</li>
                  </ul>
                </p>
              </div>
            </div>
            <br/>
            <div v-if="ActiveUser != null">
              <md-input-container>
                <label>Write your message here...</label>
                <md-input v-model="ActiveChat.message" @keyup.enter.native="sendMessage"></md-input>
              </md-input-container>
              <md-button class="md-fab md-fab-bottom-right" @click.native="sendMessage">
                <md-icon>send</md-icon>
              </md-button>
            </div>
          </div>
        </md-whiteframe>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import _ from 'lodash'
  import { getAllContacts, getMessages } from 'services/messages'

  export default {
    name: 'message-create',
    data () {
      return {
        ActiveChat: {
          message: null,
          loading: false,
          data: []
        },
        ActiveUser: null,
        Catalog: {
          loading: true,
          Users: []
        }
      }
    },
    watch: {
      'ActiveUser' (user) {
        var _self = this
        _self.$set(_self.ActiveChat, 'loading', true)
        _self.fetchMessages()
      }
    },
    computed: {
      userInfo () {
        return this.$store.getters.getUserInfo
      }
    },
    methods: {
      sendMessage () {
        console.log(this.ActiveChat)
      },
      openChat (user) {
        var _self = this
        if (!_self.ActiveUser || user.Id !== _self.ActiveUser.Id) {
          _self.$set(_self, 'ActiveUser', user)
        }
      },
      fetchMessages: _.debounce(function () {
        const _self = this
        getMessages({
          contact: _self.ActiveUser.Id
        }).then(res => {
          _self.ActiveChat = {
            data: res.data,
            loading: false,
            message: null
          }
        }).catch(err => { console.log(err.response) })
      }, 500, {
        leading: false,
        trailing: true
      })
    },
    mounted () {
      const _self = this
      getAllContacts().then(res => {
        _self.Catalog = {
          Users: res.data,
          loading: false
        }
      }).catch(err => {
        console.log(err.response)
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
    overflow: hidden;
    text-overflow: ellipsis;
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
  
  .padded-container {
    padding: 20px;
  }
  
  .scroll {
    width: 100%;
    height: 420px;
    overflow-y: scroll;
  }
  
  .chat-list {
    padding: 0;
    margin: 0;
    list-style: none;
    width: 100%;
  }
  
  .md-input-container {
    width: 94%;
  }

</style>
