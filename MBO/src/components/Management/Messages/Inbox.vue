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
          <div class="padded-container">
            <md-input-container>
              <label>Search</label>
              <md-input v-model="Filter.contact"></md-input>
            </md-input-container>
          </div>
          <ul class="contact-list">
            <li v-for="user in filteredContacts" class="full-width text-left" @click="openChat(user)" :class="{'active' : ActiveChat.user && user.Id == ActiveChat.user.Id}">
              <md-icon>account_circle</md-icon>
              <span>{{ user.Email }}</span>
            </li>
          </ul>
          <pre>{{Filter.contact}}</pre>
          <pre>{{filteredContacts}}</pre>
        </md-whiteframe>
      </md-layout>
      <md-layout>
        <md-whiteframe md-tag="section" class="full-width">
          <md-progress class="md-primary" :class="{hide: !ActiveChat.loading }" md-indeterminate></md-progress>
          <div class="padded-container">
            <div class="scroll" ref="scrollContainer">
              <div v-if="ActiveChat.user != null">
                <ul class="chat-list" v-if="ActiveChat.data.length">
                  <li v-for="chat in ActiveChat.data">
                    <pre>{{chat}}</pre>
                  </li>
                </ul>
                <div v-else>
                  <p class="text-center">
                    <span class="md-caption">Say hello!</span>
                    <p class="text-center">This is beigning of your conversation with {{ActiveChat.user.Email}}</p>
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
            <div v-if="ActiveChat.user != null" class="message-draft">
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
          user: null,
          message: null,
          loading: false,
          data: []
        },
        Catalog: {
          loading: true,
          Users: []
        },
        Filter: {
          contact: ''
        }
      }
    },
    watch: {
      'ActiveChat.user' (user) {
        var _self = this
        _self.$set(_self.ActiveChat, 'loading', true)
        _self.fetchMessages()
      }
    },
    computed: {
      userInfo () {
        return this.$store.getters.getUserInfo
      },
      filteredContacts () {
        var _self = this
        return _self.Catalog.Users.filter((item) => {
          return (item.Email.includes(_self.Filter.contact) || item.UserName.includes(_self.Filter.contact) || item.FullName.includes(_self.Filter.contact))
        })
      }
    },
    methods: {
      scrollChat () {
        this.$refs['scrollContainer'].scrollTop = this.$refs['scrollContainer'].scrollHeight
      },
      sendMessage () {
        console.log(this.ActiveChat)
      },
      openChat (user) {
        var _self = this
        if (!_self.ActiveChat.user || user.Id !== _self.ActiveChat.user.Id) {
          _self.$set(_self.ActiveChat, 'user', user)
        }
      },
      fetchMessages: _.debounce(function () {
        const _self = this
        getMessages({
          contact: _self.ActiveChat.user.Id
        }).then(res => {
          _self.$set(_self.ActiveChat, 'data', res.data)
          _self.$set(_self.ActiveChat, 'loading', false)
          _self.$set(_self.ActiveChat, 'message', null)
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
    border-bottom: 1px solid #efefef;
    color: rgba(0, 0, 0, 0.7);
    transition: background-color 0.5s ease-out;
    overflow: hidden;
    text-overflow: ellipsis;
  }
  
  .contact-list li:not(.active):hover {
    background-color: #efefef;
    color: rgba(0, 0, 0, 0.9);
  }
  
  .contact-list li.active {
    color: #3f51b5;
    background-color: rgba(63, 81, 181, 0.1);
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
  
  .message-draft .md-input-container {
    width: 94%;
  }

</style>
