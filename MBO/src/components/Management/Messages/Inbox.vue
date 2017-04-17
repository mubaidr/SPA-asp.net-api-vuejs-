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
    <md-layout md-gutter="24">
      <md-layout md-flex-small="33" md-flex="15">
        <md-whiteframe md-tag="section" class="full-width">
          <md-list class="mail-list">
            <md-list-item :disabled="ActiveFolder.loading" @click.native="openFolder('compose')" :class="{'md-primary': ActiveFolder.name == 'compose'}">
              <md-icon>mail</md-icon> <span>Compose</span>
            </md-list-item>
            <md-list-item :disabled="ActiveFolder.loading" @click.native="openFolder('inbox')" :class="{'md-primary': ActiveFolder.name == 'inbox'}">
              <md-icon>inbox</md-icon> <span>Inbox</span>
            </md-list-item>
            <md-list-item :disabled="ActiveFolder.loading" @click.native="openFolder('sent')" :class="{'md-primary': ActiveFolder.name == 'sent'}">
              <md-icon>send</md-icon> <span>Sent Mail</span>
            </md-list-item>
            <md-list-item :disabled="ActiveFolder.loading" @click.native="openFolder('trash')" :class="{'md-primary': ActiveFolder.name == 'trash'}">
              <md-icon>delete</md-icon> <span>Trash</span>
            </md-list-item>
          </md-list>
        </md-whiteframe>
      </md-layout>
      <md-layout>
        <message-create v-if="ActiveFolder.name == 'compose'"></message-create>
        <md-whiteframe md-tag="section" class="full-width" v-else>
          <pagination :full-width="true" :lastpage="ActiveFolder.lastPage" :loading="ActiveFolder.loading" :count="ActiveFolder.data.length"
            :view-menu="false" :sort-menu="false" :refresh-menu="true" @refresh="search"></pagination>
          <md-list class="md-double-line" v-if="ActiveFolder.data.length">
            <md-list-item v-for="chat in ActiveFolder.data">

              <div class="md-list-text-container">
                <span>{{chat.Sender.Email}}</span>
                <span>{{chat.Description}}</span>
              </div>

              <md-button class="md-icon-button md-list-action">
                <md-icon>delete</md-icon>
              </md-button>
              <md-button class="md-icon-button md-list-action">
                <md-icon>markunread</md-icon>
              </md-button>
              <md-button class="md-icon-button md-list-action">
                <md-icon>reply</md-icon>
              </md-button>
            </md-list-item>
          </md-list>
          <div class="flex-vertical min-height full-width" v-else>
            <div class="no-content">
              <md-icon class="md-accent md-size-2x">cloud_queue</md-icon><br/>
              <p v-if="ActiveFolder.loading">Loading...</p>
              <p v-else>Awww... Nothing here!</p>
              <span v-show="ActiveFolder.error">An error occured while trying to fetch data.</span>
            </div>
          </div>
        </md-whiteframe>
      </md-layout>
    </md-layout>
    <pre>{{ActiveFolder.data}}</pre>
  </div>
</template>
<script>
  import _ from 'lodash'
  import { getMessages, postMessage } from 'services/messages'
  import pagination from 'components/_custom/pagination.vue'
  import messageCreate from 'components/Management/Messages/_partial_create.vue'

  export default {
    data () {
      return {
        ActiveFolder: {
          name: 'inbox',
          filter: '',
          loading: false,
          lastPage: 1,
          data: [],
          error: null
        }
      }
    },
    components: {
      'message-create': messageCreate,
      'pagination': pagination
    },
    watch: {},
    computed: {
      userInfo () {
        return this.$store.getters.getUserInfo
      }
    },
    methods: {
      search () {
        const _self = this
        console.log('searching...', _self)
      },
      sendMessage () {
        const _self = this

        if (_self.ActiveChat.message.trim() !== '' && _self.ActiveChat.message !== _self.lastMessage) {
          var msg = _self.createMessage()
          _self.lastMessage = _self.ActiveChat.message
          postMessage(msg).then(res => {
            // var msgs = _self.ActiveChat.data
            // msgs.push(msg)
            // this.scrollChat()
          }).catch(err => { console.log(err.data) })
        }
      },
      openFolder (folder) {
        const _self = this
        _self.ActiveFolder.name = folder
        if (folder !== 'compose') {
          _self.ActiveFolder.loading = true
          _self.fetchMessages()
        }
      },
      fetchMessages: _.debounce(function () {
        const _self = this
        getMessages({
          folder: _self.ActiveFolder.name
        }).then(res => {
          _self.ActiveFolder.data = res.data.message
          _self.ActiveFolder.loading = false
        }).catch(err => { console.log(err.data) })
      }, 500, {
        leading: false,
        trailing: true
      }),
      createMessage () {
        const _self = this
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
    padding: 0;
  }
  
  .padded-container {
    padding: 20px;
  }

</style>
