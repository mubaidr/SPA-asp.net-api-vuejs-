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
        <transition name="slide-right">
          <message-create v-if="ActiveFolder.name == 'compose'" @message-sent="messageSent"></message-create>
          <md-whiteframe class="min-height full-width" md-tag="section" v-else>
            <pagination :full-width="true" :lastpage="ActiveFolder.lastPage" :loading="ActiveFolder.loading" :count="ActiveFolder.data.length" :view-menu="false" :sort-menu="false" :refresh-menu="true" @refresh="search"></pagination>
            <md-list class="md-double-line md-custom-inbox" v-if="ActiveFolder.data.length">              
              <md-list-item v-for="chat in ActiveFolder.data">
                <!--TODO add notification for unread time-->
                <div class="md-list-text-container" :class="{'unread': !chat.IsRead && chat.SenderID != userInfo.ID}">
                  <span>{{chat.Description}}</span>
                  <span class="small">{{chat.Sender.Email}}</span>
                </div>
                <div>
                  <span class="md-caption">{{formatDate(chat.Time)}}</span>
                </div>
                <md-button class="md-icon-button md-list-action" v-show="chat.Sender.Id !== userInfo.ID">
                  <md-icon class="md-accent">reply</md-icon>
                </md-button>
                <md-button class="md-icon-button md-list-action" @click.native="removeMessage(chat.MessageID)">
                  <md-icon class="md-accent">delete</md-icon>
                </md-button>
              </md-list-item>
            </md-list>
            <div class="flex-vertical min-height full-width" v-else>
              <div class="no-content">
                <md-icon class="md-accent md-size-2x">cloud_queue</md-icon><br>
                <p v-if="ActiveFolder.loading">Loading...</p>
                <p v-else="">Awww... Nothing here!</p>
                <span v-show="ActiveFolder.error">An error occurred while trying to fetch data.</span>
              </div>
            </div>
          </md-whiteframe>
        </transition>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import _ from 'lodash'
  import { getMessages, deleteMessage, markReadMessage } from 'services/messages'
  import pagination from 'components/_custom/pagination.vue'
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
      readMessage (id) {
        markReadMessage(id).catch(() => {
          console.log('some error')
        })
      },
      removeMessage (id) {
        this.ActiveFolder.loading = true
        deleteMessage(id).then(() => {
          this.openFolder(this.ActiveFolder.name)
        }).catch(err => {
          console.log(err)
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
        const _self = this
  
        var pagingOptions = {}
        pagingOptions.folder = _self.ActiveFolder.name
        pagingOptions.filter = obj.filter
        pagingOptions.orderby = obj.orderby
        pagingOptions.page = obj.page

        _self.ActiveFolder.loading = true
        getMessages(pagingOptions).then(res => {
          _self.ActiveFolder.data = res.data.message
          _self.ActiveFolder.count = res.data.count
          _self.ActiveFolder.lastPage = res.data.last_page
        })
        .catch(err => {
          console.log(err.data)
        })
        .then(() => {
          _self.ActiveFolder.loading = false
        })
      },
      openFolder (folder) {
        const _self = this
        _self.ActiveFolder.name = folder
        if (folder !== 'compose') {
          _self.ActiveFolder.data = []
          _self.ActiveFolder.loading = true
          _self.ActiveFolder.filter = ''
          _self.ActiveFolder.lastPage = 1
          _self.ActiveFolder.count = 0
          _self.fetchMessages()
        }
      },
      fetchMessages: _.debounce(function () {
        const _self = this
        getMessages({
          folder: _self.ActiveFolder.name
        }).then(res => {
          _self.ActiveFolder.data = res.data.message
          _self.ActiveFolder.count = res.data.count
          _self.ActiveFolder.lastPage = res.data.last_page
        }).catch(err => { console.log(err.data) }).then(() => {
          _self.ActiveFolder.loading = false
        })
      }, 500, {
        leading: false,
        trailing: true
      })
    },
    created () {
      this.openFolder('inbox')
    },
    mounted () {
      // const _self = this
    }
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
