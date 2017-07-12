<template>
  <div>
    <h1>
      <span>New Task</span>
      <br/>
      <span class="md-caption">Get started</span>
    </h1>
    <md-whiteframe md-tag="section" class="form">
      <md-progress class="md-accent" :class="{'hidden': !state.loading}" md-indeterminate></md-progress>
      <div>
        <md-input-container :class="{'md-input-invalid': errors.has('Title')}">
          <label>Title</label>
          <md-input name="Title" data-vv-name="Title" data-vv-rules="required|min:5" type="text" v-model="Task.Title" maxlength="50" v-validate></md-input>
          <span class="md-error">{{errors.first('Title')}}</span>
        </md-input-container>
        <md-input-container>
          <label>Details
            <span>(optional)</span>
          </label>
          <md-textarea v-model="Task.Description" maxlength="200" name="Description"></md-textarea>
        </md-input-container>
        <md-input-container>
          <label for="Categories">Category</label>
          <md-select name="Categories" v-model="Task.CategoryID">
            <md-option v-for="category in Catalog.Categories" :value="category.CategoryID" :title="category.Description" :key="category.Id">{{category.Title}}</md-option>
          </md-select>
        </md-input-container>
        <md-input-container>
          <label for="Users">Assign To</label>
          <md-select name="Users" multiple v-model="Users">
            <md-option v-for="user in Catalog.Users" :value="user.Id" :title="user.UserName" :key="user.Id">{{user.UserName}}</md-option>
          </md-select>
        </md-input-container>
        <label class="custom-label text-muted" for="DateDue">Target Date</label>
        <br>
        <datepicker v-model="Task.DateDue" inline format="DD-MM-YYYY" initial-view="month" :required="true" :disabled="{days: [6, 0]}"></datepicker>
        <div class="clearfix">
          <md-button class="md-accent" @click.native="$router.push({ path: '/tasks' })">
            View All Tasks
          </md-button>
          <md-button class="md-accent" @click.native="$router.push({ path: '/dashboard' })">
            Dashboard
          </md-button>
          <md-button class="md-raised md-accent" id="btn-Submit" @click.native="formValidate" :disabled="state.loading">Create New Task</md-button>
        </div>
      </div>
    </md-whiteframe>
  </div>
</template>
<script>
import tasks from 'services/tasks'
import account from 'services/account'
import catalogs from 'services/catalogs'

import Datepicker from 'vuejs-datepicker'
// import moment from 'moment'

export default {
  name: 'task-create',
  components: {
    Datepicker
  },
  data () {
    return {
      state: {
        loading: false
      },
      Task: {
        Title: '',
        Description: '',
        CategoryID: 1,
        DateDue: ''
      },
      Users: [],
      Catalog: {
        Categories: [],
        Users: []
      }
    }
  },
  methods: {
    formValidate (event) {
      event.preventDefault()

      this.$validator.validateAll().then(success => {
        if (!success) return
        this.state.loading = true

        tasks.create({
          mainTask: this.Task,
          users: this.Users
        }).then(res => {
          this.$router.push({
            path: '/tasks'
          })
        }).catch(err => {
          this.$toast.error({
            title: err.response.data.Message,
            message: err.response.data.ModelState
          })
        }).then(() => {
          this.state.loading = false
        })
      })
    }
  },
  created () {
    var myDate = new Date()
    this.Task.DateDue = new Date(myDate.setTime(myDate.getTime() + 7 * 86400000))

    catalogs.getCategories().then(res => {
      this.Catalog.Categories = res.data
    }).catch(() => {
      this.$toast.error({
        title: 'Error',
        message: 'Unable to fetch categories'
      })
    })

    account.getUsers().then(res => {
      this.Catalog.Users = res.data
    }).catch(() => {
      this.$toast.error({
        title: 'Error',
        message: 'Unable to fetch users list'
      })
    })
  }
}

</script>
<style>
  .compact-card .md-card-content,
  .compact-card .md-card-actions {
    padding: 0;
  }

  .compact-card .md-card-actions {
    padding-top: 16px;
  }

  .compact-card .md-card {
    box-shadow: none;
  }

  .vdp-datepicker__calendar {
    /*Customize datepicker*/
    margin: 0px auto!important;
    width: 100%!important;
    border-color: rgba(0, 0, 0, 0.12)!important;
    border-radius: 3px;
  }

  .vdp-datepicker__calendar .cell:not(.blank):hover {
    border-color: #3f51b5!important;
  }

  .vdp-datepicker__calendar .cell.selected,
  .vdp-datepicker__calendar .cell.selected.highlighted,
  .vdp-datepicker__calendar .cell.selected:hover {
    color: #fff!important;
    background-color: #3f51b5!important;
  }
</style>
