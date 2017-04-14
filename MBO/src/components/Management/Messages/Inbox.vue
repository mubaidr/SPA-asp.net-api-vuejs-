<template>
  <div>

    <md-toolbar class="md-transparent md-dense">
      <div class="md-toolbar-container">
        <md-button class="md-icon-button md-accent" @click.native="$router.push({path: '/dashboard' })">
          <md-tooltip md-direction="top">Dashboard</md-tooltip>
          <md-icon>arrow_back</md-icon>
        </md-button>
        <span>Inbox</span>
        <span style="flex: 1"></span>
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
      </div>
    </md-toolbar>
    <md-whiteframe md-tag="section" class="full-width">
      <md-toolbar class="md-transparent md-dense">
        <div class="md-toolbar-container">
          <md-button class="md-accent">
            <md-tooltip md-direction="top">Create new message</md-tooltip>
            <md-icon>mail</md-icon>
            <span>Compose</span>
          </md-button>
          <span style="flex: 1"></span>
          <md-button class="md-accent" @click.native="$router.push({path: '/tasks'})">
            <md-tooltip md-direction="top">Refresh</md-tooltip>
            <md-icon>refresh</md-icon>
            Refresh
          </md-button>
        </div>
      </md-toolbar>
    </md-whiteframe>
    <br />
    <md-layout md-gutter="24">
      <md-layout md-flex-small="33" md-flex="15">
        <md-whiteframe md-tag="section" class="full-width">
          <md-list class="mail-list">
            <md-list-item @click.native="openFolder('inbox')" :class="{'md-primary': ActiveFolder == 'inbox'}">
              <md-icon>inbox</md-icon> <span>Inbox</span>
            </md-list-item>
            <md-list-item @click.native="openFolder('inbox')" :class="{'md-primary': ActiveFolder == 'sent'}">
              <md-icon>send</md-icon> <span>Sent Mail</span>
            </md-list-item>
            <md-list-item @click.native="openFolder('inbox')" :class="{'md-primary': ActiveFolder == 'trash'}">
              <md-icon>delete</md-icon> <span>Trash</span>
            </md-list-item>
          </md-list>
        </md-whiteframe>
      </md-layout>
      <md-layout>
        <md-whiteframe md-tag="section" class="full-width">
          <md-progress class="md-primary" :class="{hide: !ActiveFolder.loading }" md-indeterminate></md-progress>
          <div class="padded-container">
            {{ActiveFolder.data}}
          </div>
        </md-whiteframe>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import _ from 'lodash'
  import { getMessages, postMessage } from 'services/messages'

  export default {
    name: 'message-create',
    data () {
      return {
        ActiveFolder: {
          name: 'inbox',
          filter: '',
          loading: false,
          data: []
        }
      }
    },
    watch: {},
    computed: {
      userInfo () {
        return this.$store.getters.getUserInfo
      }
    },
    methods: {
      sendMessage () {
        var _self = this

        if (_self.ActiveChat.message.trim() !== '' && _self.ActiveChat.message !== _self.lastMessage) {
          var msg = _self.createMessage()
          _self.lastMessage = _self.ActiveChat.message
          postMessage(msg).then(res => {
            // var msgs = _self.ActiveChat.data
            // msgs.push(msg)
            // _self.$set(_self.ActiveChat, 'data', msgs)
            // _self.$set(_self.ActiveChat, 'message', null)
            // this.scrollChat()
          }).catch(err => { console.log(err.data) })
        }
      },
      openFolder (folder) {
        var _self = this
        _self.$set(_self.ActiveFolder, 'name', folder)
        _self.$set(_self.ActiveFolder, 'loading', true)
        _self.fetchMessages()
      },
      fetchMessages: _.debounce(function () {
        const _self = this
        getMessages({
          folder: _self.ActiveFolder.name
        }).then(res => {
          _self.$set(_self.ActiveFolder, 'data', res.data.message)
          _self.$set(_self.ActiveChat, 'loading', false)
        }).catch(err => { console.log(err.data) })
      }, 500, {
        leading: false,
        trailing: true
      }),
      createMessage () {
        var _self = this
        return {
          Description: _self.ActiveChat.message,
          ReceiverID: _self.ActiveChat.user.Id,
          Sender: _self.userInfo,
          Receiver: _self.ActiveChat.user,
          Time: new Date()
        }
      }
    },
    mounted () {
      // const _self = this
    }
  }

</script>
<style scoped>
  .mail-list {
    list-style: none;
    padding: 0;
    margin: 0;
    width: 100%;
  }
  
  .mail-list li {
    cursor: pointer;
    overflow: hidden;
    text-overflow: ellipsis;
  }
  
  .mail-list li:not(.active):hover {
    background-color: #efefef;
    color: rgba(0, 0, 0, 0.9);
  }
  
  .mail-list li.active {
    color: #3f51b5;
    background-color: rgba(63, 81, 181, 0.1);
  }
  
  .padded-container {
    padding: 20px;
  }
  
  .scroll {
    width: 100%;
    height: 320px;
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
  
  .chat-item {
    margin-right: 20px;
    padding: 10px;
    text-align: right;
  }
  
  .chat-item.left {
    text-align: left;
    background-color: rgba(63, 81, 181, 0.1);
  }

</style>
