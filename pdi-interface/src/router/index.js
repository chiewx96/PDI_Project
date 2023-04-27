import { createRouter, createWebHashHistory } from "vue-router";
import store from "@/store";
import LoginView from "@/views/LoginView.vue";

const routes = [
  {
    path: "/",
    name: "Login",
    component: LoginView,
  },
  {
    path: "/scan",
    name: "Scan",
    component: () => import("../views/ScanView.vue"),
    meta: {
      authRequired: true,
    },
  },
  {
    path: "/cancel",
    name: "Cancel",
    component: () => import("../views/CancelView.vue"),
    meta: {
      authRequired: true,
    },
  },
  {
    path: "/assign-container",
    name: "AssignContainer",
    component: () => import("../views/UpdateContainerView.vue"),
    meta: {
      authRequired: true,
    },
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  // this route requires auth, check if logged in
  // if not, redirect to login page.
  if (to.matched.some((record) => record.meta.authRequired)) {
    if (!store.getters.isLoggedIn) {
      next({ name: "Login" });
    } else {
      next(); // go to wherever I'm going
    }
  } else {
    next(); // go to wherever I'm going
  }
});

export default router;
