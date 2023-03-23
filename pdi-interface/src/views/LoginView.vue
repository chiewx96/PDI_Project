<template>
  <form id="login">
    <h1>Login</h1>
    <div class="form-inputs">
      <label for="username">Username</label>
      <input
        type="text"
        id="username"
        name="username"
        v-model="input.username"
        placeholder="Username"
      />
    </div>
    <div class="form-inputs">
      <label for="password">Password</label>
      <input
        type="password"
        id="password"
        name="password"
        v-model="input.password"
        placeholder="Password"
      />
    </div>
    <button
      type="button"
      v-on:click="login()"
    >
      Login
    </button>
  </form>
</template>

<script>
import ApiService from '@/services/api.service';
import { mapMutations } from 'vuex';

export default {
  name: 'LoginView',
  data() {
    return {
      input: {
        username: '',
        password: '',
      },
    };
  },
  methods: {
    ...mapMutations(['setUser', 'setToken']),
    login() {
      if (this.input.username != '' && this.input.password != '') {
        // This should actually be an api call not a check against this.$parent.mockAccount
        ApiService._post('user/login', {
          username: this.input.username,
          password: this.input.password,
        }).then(async (response) => {
          if (response.status == 200) {
            const { status, message } = await response.json();
            if (status == true) {
              this.setUser(message.user);
              this.setToken(message.token);
              this.$router.push('/scan');
            }
          }
        });
      } else {
        console.log('A username and password must be present');
      }
    },
  },
};
</script>

<style>
#login .form-inputs {
  padding-bottom: 10px;
}
#login .form-inputs label {
  padding-right: 10px;
}
</style>
