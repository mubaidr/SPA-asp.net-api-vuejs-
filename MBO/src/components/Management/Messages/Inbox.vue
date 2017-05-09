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
        <md-whiteframe class="full-width" md-tag="section">
          <md-list class="mail-list">
            <md-list-item :disabled="ActiveFolder.loading || ActiveFolder.name == 'compose'" @click.native="openFolder('compose')" :class="ActiveFolder.name == 'compose' ? 'md-primary': ''">
              <md-icon>mail</md-icon>
              <span>Compose</span>
            </md-list-item>
            <md-list-item :disabled="ActiveFolder.loading || ActiveFolder.name == 'inbox'" @click.native="openFolder('inbox')" :class="ActiveFolder.name == 'inbox' ? 'md-primary': ''">
              <md-icon>inbox</md-icon>
              <span>Inbox</span>
            </md-list-item>
            <md-list-item :disabled="ActiveFolder.loading || ActiveFolder.name == 'sent'" @click.native="openFolder('sent')" :class="ActiveFolder.name == 'sent' ? 'md-primary': ''">
              <md-icon>send</md-icon>
              <span>Sent Mail</span>
            </md-list-item>
            <md-list-item :disabled="ActiveFolder.loading || ActiveFolder.name == 'trash'" @click.native="openFolder('trash')" :class="ActiveFolder.name == 'trash' ? 'md-primary': ''">
              <md-icon>delete</md-icon>
              <span>Trash</span>
            </md-list-item>
          </md-list>
        </md-whiteframe>
      </md-layout>
      <md-layout>
        <transition name="slide-right" appear>
          <message-create v-if="ActiveFolder.name == 'compose'" :message="Message" @message-sent="messageSent"></message-create>
          <md-whiteframe class="min-height full-width" md-tag="section" v-else>
            <pagination :full-width="true" :lastpage="ActiveFolder.lastPage" :loading="ActiveFolder.loading" :count="ActiveFolder.data.length" :view-menu="false" :sort-menu="false" :refresh-menu="true" @refresh="search"></pagination>
            <transition name="slide-up" appear mode="out-in">
              <md-list class="md-double-line md-custom-inbox" v-if="ActiveFolder.data.length">
                <md-list-item v-for="chat in ActiveFolder.data" @click.native="readMessage(chat)" :key="chat.ID">
                  <div class="md-list-text-container" :class="{'unread': !chat.IsRead && chat.SenderID != userInfo.ID}">
                    <span>{{chat.Description}}</span>
                    <span class="small" v-if="ActiveFolder.name === 'inbox'">{{chat.Sender.Email}}</span>
                    <span class="small" v-else>{{chat.Receiver.Email}}</span>
                  </div>
                  <div>
                    <span class="md-caption">{{formatDate(chat.Time)}}</span>
                  </div>
                  <md-button class="md-icon-button md-list-action" v-show="chat.Sender.Id !== userInfo.ID" @click.native="replyMessage(chat)">
                    <md-icon class="md-accent">reply</md-icon>
                  </md-button>
                  <md-button class="md-icon-button md-list-action" @click.native="removeMessage(chat.MessageID)" v-if="!chat.IsDeleted">
                    <md-icon class="md-accent">delete</md-icon>
                  </md-button>
                  <md-button class="md-icon-button md-list-action" @click.native="removeMessage(chat.MessageID)" v-else>
                    <md-icon class="md-accent">restore</md-icon>
                  </md-button>
                </md-list-item>
              </md-list>
              <data-state :loading="ActiveFolder.loading" :error="ActiveFolder.error" v-else></data-state>
            </transition>
          </md-whiteframe>
        </transition>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
import _ from 'lodash'
import { getMessages, deleteMessage, restoreMessage, markReadMessage } from 'services/messages'
import pagination from 'components/_custom/pagination.vue'
import dataState from 'components/_custom/data-state.vue'
import messageCreate from 'components/Management/Messages/_partial_create.vue'
import moment from 'moment'

export default {
  data () {
    return {
      ActiveFolder: {
        name: 'inbox',
        filter: '',
        loading: false,
        lastPage: 1,
        count: 0,
        data: [],
        error: null
      },
      Message: {
        Description: '',
        User: ''
      }
    }
  },
  components: {
    messageCreate,
    pagination,
    dataState
  },
  watch: {
    '$route.query.folder' (folder) {
      this.openFolder(folder)
    }
  },
  computed: {
    userInfo () {
      return this.$store.getters.getUserInfo
    }
  },
  methods: {
    replyMessage (chat) {
      this.Message.Description = chat.Description
      this.Message.User = chat.SenderID
      this.openFolder('compose')
    },
    readMessage (msg) {
      if (!msg.IsRead) {
        markReadMessage(msg.MessageID).catch(err => {
          this.setErrorDetails(err)
        })
      }
    },
    unDeleteMessage (id) {
      this.ActiveFolder.loading = true
      restoreMessage(id).then(() => {
        this.openFolder(this.ActiveFolder.name)
      }).catch(err => {
        this.setErrorDetails(err)
      }).then(() => {
        this.ActiveFolder.loading = false
      })
    },
    removeMessage (id) {
      this.ActiveFolder.loading = true
      deleteMessage(id).then(() => {
        this.openFolder(this.ActiveFolder.name)
      }).catch(err => {
        this.setErrorDetails(err)
      }).then(() => {
        this.ActiveFolder.loading = false
      })
    },
    messageSent () {
      this.openFolder('inbox')
    },
    formatDate (date) {
      return moment(date).format('hh:mmA DD-MM-YYYY')
    },
    search (obj) {
      var pagingOptions = {}
      pagingOptions.folder = this.ActiveFolder.name
      pagingOptions.filter = obj.filter
      pagingOptions.orderby = obj.orderby
      pagingOptions.page = obj.page

      this.ActiveFolder.loading = true
      getMessages(pagingOptions).then(res => {
        this.ActiveFolder.data = res.data.message
        this.ActiveFolder.count = res.data.count
        this.ActiveFolder.lastPage = res.data.last_page
      })
        .catch(err => {
          this.setErrorDetails(err)
        })
        .then(() => {
          this.ActiveFolder.loading = false
        })
    },
    openFolder (folder) {
      this.ActiveFolder.name = folder
      if (folder !== 'compose') {
        this.Message = {}
        this.ActiveFolder.data = []
        this.ActiveFolder.loading = true
        this.ActiveFolder.filter = ''
        this.ActiveFolder.lastPage = 1
        this.ActiveFolder.count = 0
        this.fetchMessages()
      }
    },
    fetchMessages: _.debounce(function () {
      getMessages({
        folder: this.ActiveFolder.name
      }).then(res => {
        this.ActiveFolder.data = res.data.message
        this.ActiveFolder.count = res.data.count
        this.ActiveFolder.lastPage = res.data.last_page
      }).catch(err => {
        this.setErrorDetails(err)
      }).then(() => {
        this.ActiveFolder.loading = false
      })
    },
      500,
      {
        leading: false,
        trailing: true
      })
  },
  created () {
    var folder = this.$route.query.folder || 'inbox'
    this.openFolder(folder)
  },
  mounted () { }
}

</script>
<style scoped="">
.mail-list {
  padding: 0;
}

.padded-container {
  padding: 20px;
}

.md-custom-inbox {
  padding: 0;
}

.md-custom-inbox .md-list-item {
  cursor: pointer;
}

.md-custom-inbox .md-list-item:hover {
  background-color: #efefef;
}

.md-custom-inbox .md-list-item .unread span {
  color: #3f51b5;
}

.small {
  font-size: 0.75em;
}
</style>
