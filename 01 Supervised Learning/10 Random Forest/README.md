# Decision Tree (CART) — Mathematical Formulation + Code

## Core Idea

At each node, choose feature ( j ) and split threshold ( t ) that minimizes impurity.

### Classification (Gini Impurity)

$$
[
Gini(S) = 1 - \sum_{k=1}^{K} p_k^2
]$$

$$[
(j^*, t^*) = \arg\min_{j,t} \left( \frac{|S_L|}{|S|} Gini(S_L) + \frac{|S_R|}{|S|} Gini(S_R) \right)
]$$

### Regression (MSE)

[
MSE(S) = \frac{1}{|S|} \sum_{i \in S} (y_i - \bar{y})^2
]

---

### Python Implementation (Decision Tree - Classification)

```python
import numpy as np

class DecisionTree:
    def __init__(self, max_depth=5):
        self.max_depth = max_depth

    def gini(self, y):
        classes, counts = np.unique(y, return_counts=True)
        probs = counts / counts.sum()
        return 1 - np.sum(probs ** 2)

    def best_split(self, X, y):
        m, n = X.shape
        best_gini = float("inf")
        best_idx, best_thr = None, None

        for j in range(n):
            thresholds = np.unique(X[:, j])
            for t in thresholds:
                left = y[X[:, j] <= t]
                right = y[X[:, j] > t]

                if len(left) == 0 or len(right) == 0:
                    continue

                g = (len(left)/m)*self.gini(left) + (len(right)/m)*self.gini(right)

                if g < best_gini:
                    best_gini = g
                    best_idx = j
                    best_thr = t

        return best_idx, best_thr

    def build(self, X, y, depth=0):
        if depth >= self.max_depth or len(set(y)) == 1:
            return {"label": np.bincount(y).argmax()}

        idx, thr = self.best_split(X, y)
        if idx is None:
            return {"label": np.bincount(y).argmax()}

        left_mask = X[:, idx] <= thr
        right_mask = X[:, idx] > thr

        return {
            "feature": idx,
            "threshold": thr,
            "left": self.build(X[left_mask], y[left_mask], depth+1),
            "right": self.build(X[right_mask], y[right_mask], depth+1)
        }

    def fit(self, X, y):
        self.tree = self.build(X, y)

    def predict_one(self, x, node):
        if "label" in node:
            return node["label"]
        if x[node["feature"]] <= node["threshold"]:
            return self.predict_one(x, node["left"])
        else:
            return self.predict_one(x, node["right"])

    def predict(self, X):
        return np.array([self.predict_one(x, self.tree) for x in X])
```

---

## Random Forest — Mathematical Formulation + Code

### Core Idea

Ensemble of ( B ) trees trained on bootstrapped datasets with feature randomness.

### Bootstrapping

[
D_b \sim \text{Sample with replacement from } D
]

### Prediction (Classification - Majority Vote)

[
\hat{y} = \text{mode} { T_b(x) }_{b=1}^{B}
]

### Feature Subsampling

At each split, randomly select ( m ) features from total ( p ):
[
m \approx \sqrt{p}
]

---

### Python Implementation (Random Forest)

```python
import numpy as np

class RandomForest:
    def __init__(self, n_trees=10, max_depth=5):
        self.n_trees = n_trees
        self.max_depth = max_depth
        self.trees = []

    def bootstrap(self, X, y):
        n = len(X)
        idx = np.random.choice(n, n, replace=True)
        return X[idx], y[idx]

    def fit(self, X, y):
        self.trees = []
        for _ in range(self.n_trees):
            X_sample, y_sample = self.bootstrap(X, y)
            tree = DecisionTree(max_depth=self.max_depth)
            tree.fit(X_sample, y_sample)
            self.trees.append(tree)

    def predict(self, X):
        preds = np.array([tree.predict(X) for tree in self.trees])
        return np.apply_along_axis(lambda x: np.bincount(x).argmax(), axis=0, arr=preds)
```

---

## Key Mathematical Summary

* Tree learns:
  [
  \min_{j,t} ; Impurity(S_L, S_R)
  ]

* Forest learns:
  [
  \hat{y} = \frac{1}{B} \sum_{b=1}^{B} T_b(x) \quad (\text{regression})
  ]

[
\hat{y} = \text{mode}(T_b(x)) \quad (\text{classification})
]

* Variance reduction:
  [
  Var(\text{RF}) \approx \frac{1}{B} Var(\text{Tree})
  ]

---

If you want, next step I can give:

* **Fully optimized (vectorized) version**
* **C++ implementation (faster for your Drogon use-case)**
* **How sklearn differs internally (important for interviews)**
