# Linear Regression of One Variable
==============================

In linear regression, the goal is to build a mathematical model that predicts the value of a continuous outcome variable based on one or more predictor variables. The model aims to minimize the difference between the predicted values and the actual values of the outcome variable.

The simplest form of linear regression is called simple linear regression, which involves only one predictor variable. In this case, the goal is to build a linear equation that best predicts the value of the outcome variable based on the value of the predictor variable.

The assumptions for simple linear regression are:

1. Linearity: The relationship between the outcome variable and the predictor variable is linear.
2. Independence: Each observation is independent of the others.
3. Homoscedasticity: The variance of the residuals is constant across all levels of the predictor variable.
4. Normality: The residuals are normally distributed.
5. No multicollinearity: There are no perfect linear relationships between any two predictor variables.

This is an inline equation:
Conside have sample input (x_)
$$y_i=\beta_1*x_i+\beta_0$$
```math
$$V_{sphere} = \frac{4}{3}\pi r^3$$
```
The model for simple linear regression is given by:

This is an inline equation: $$V_{sphere} = \frac{4}{3}\pi r^3$$,<br>
followed by a display style equation:

$$V_{sphere} = \frac{4}{3}\pi r^3$$

`latex
Linear Regression Equation:

\[ Y = \beta_0 + \beta_1X + \varepsilon \]

Where:
- \( \beta_0 \) is the y-intercept (the value of \( Y \) when \( X = 0 \)).
- \( \beta_1 \) is the slope of the regression line (the change in \( Y \) for a one-unit change in \( X \)).
- \( \varepsilon \) represents the error term, which captures the difference between the observed values of \( Y \) and the values predicted by the regression line.

`

Where y is the outcome variable, x is the predictor variable, β0 is the intercept or constant term, β1 is the slope coefficient, and ε is the error term.

The parameters β0 and β1 are estimated using a method called ordinary least squares (OLS). The OLS method minimizes the sum of the squared errors between the predicted values and the actual values of the outcome variable.

Once the parameters have been estimated, we can use them to make predictions for new observations. We do this by plugging in the values of the predictor variables into the equation and solving for the value of the outcome variable.

Simple linear regression is a powerful tool for exploring the relationship between a single predictor variable and an outcome variable. It is widely used in many fields, including finance, economics, social sciences, and engineering.